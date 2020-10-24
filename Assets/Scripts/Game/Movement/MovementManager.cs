using Robbi.Debugging.Logging;
using Robbi.Events;
using Robbi.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

using Event = Robbi.Events.Event;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Manager")]
    public class MovementManager : MonoBehaviour
    {
        #region Waypoint

        private class Waypoint
        {
            public Vector3Int gridPosition;
            public GameObject destinationMarkerInstance;

            public Waypoint(Vector3Int gridPosition, GameObject destinationMarkerInstance)
            {
                this.gridPosition = gridPosition;
                this.destinationMarkerInstance = destinationMarkerInstance;
            }

            public void OnRemoved()
            {
                GameObject.Destroy(destinationMarkerInstance);
            }
        }

        #endregion

        #region Properties and Fields

        public bool DebugMovement
        {
            set
            {
                movementTilemap.GetComponent<TilemapRenderer>().enabled = value;
            }
        }

        [Header("Tilemaps")]
        public Tilemap movementTilemap;
        public Tilemap doorsTilemap;
        
        [Header("Events")]
        public Vector3Value playerLocalPosition;
        public Vector3IntEvent onMovedTo;
        public Vector3IntEvent onMovedFrom;
        public Vector3IntEvent onWaypointPlaced;
        public Vector3IntEvent onWaypointRemoved;
        
        [Header("Parameters")]
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;
        public BoolValue isProgramRunning;
        public BoolValue waypointUnreachable;

        [Header("Other")]
        public GameObject destinationMarkerPrefab;
        public float speed = 1;

        private Grid grid;
        private List<Waypoint> waypoints = new List<Waypoint>();
        private Stack<Vector3> stepsToNextWaypoint = new Stack<Vector3>();

        #endregion

        #region Unity Methods

        private void Start()
        {
            grid = movementTilemap.layoutGrid;
            waypointsPlaced.value = 0;
            isProgramRunning.value = false;
        }

        private void Update()
        {
            if (isProgramRunning.value)
            {
                if (stepsToNextWaypoint.Count > 0)
                {
                    // We are moving towards our next waypoint along the steps
                    Vector3 playerLocalPos = playerLocalPosition.value;
                    Vector3 nextStepPosition = stepsToNextWaypoint.Peek();
                    Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, speed * Time.deltaTime);

                    Vector3Int movedFrom = new Vector3Int(Mathf.RoundToInt(playerLocalPos.x - 0.5f), Mathf.RoundToInt(playerLocalPos.y - 0.5f), Mathf.RoundToInt(playerLocalPos.z));
                    Vector3Int movedTo = new Vector3Int(Mathf.RoundToInt(newPosition.x - 0.5f), Mathf.RoundToInt(newPosition.y - 0.5f), Mathf.RoundToInt(newPosition.z));
                    
                    if (newPosition == playerLocalPosition.value)
                    {
                        onMovedTo.Raise(movedTo);

                        // This step of movement is completed
                        stepsToNextWaypoint.Pop();

                        if (stepsToNextWaypoint.Count == 0)
                        {
                            // We have reached the next waypoint
                            ConsumeWaypoint(0);
                            MoveToNextWaypoint();
                        }
                    }
                    else
                    {
                        playerLocalPosition.value = newPosition;

                        if (movedFrom != movedTo)
                        {
                            onMovedFrom.Raise(movedFrom);
                        }
                    }
                }
                else
                {
                    waypointUnreachable.value = true;
                    isProgramRunning.value = false;
                }
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            isProgramRunning.value = waypoints.Count > 0;
            
            if (isProgramRunning.value)
            {
                stepsToNextWaypoint = CalculateGridSteps(grid.LocalToCell(playerLocalPosition.value), waypoints[0].gridPosition);
            }
        }

        public void AddWaypoint(Vector3 waypointWorldPosition)
        {
            if (isProgramRunning.value)
            {
                // Cannot add waypoints whilst movement program is running
                return;
            }

            Vector3Int waypointGridPosition = grid.WorldToCell(waypointWorldPosition);
            if (WaypointExists(waypointGridPosition))
            {
                // Cannot add waypoints to a position that already has one
                return;
            }

            if (remainingWaypointsPlaceable.value <= 0)
            {
                // Cannot add waypoints if we have run out of our allotted amount
                return;
            }

            Vector3Int lastWaypointGridPosition = waypoints.Count != 0 ? waypoints[waypoints.Count - 1].gridPosition : grid.WorldToCell(playerLocalPosition.value); ;

            if (waypointGridPosition != lastWaypointGridPosition && movementTilemap.HasTile(waypointGridPosition))
            {
                GameObject destinationMarkerInstance = GameObject.Instantiate(destinationMarkerPrefab, movementTilemap.transform);
                destinationMarkerInstance.transform.position = grid.GetCellCenterLocal(waypointGridPosition);

                waypoints.Add(new Waypoint(waypointGridPosition, destinationMarkerInstance));
                --remainingWaypointsPlaceable.value;
                ++waypointsPlaced.value;
                onWaypointPlaced.Raise(waypointGridPosition);

                HudLogger.LogInfo(string.Format("Waypoint added at {0}", waypointGridPosition));
            }
        }

        private bool WaypointExists(Vector3Int waypointGridPosition)
        {
            int duplicateIndex = 0;

            foreach (Waypoint waypoint in waypoints)
            {
                if (waypoint.gridPosition != waypointGridPosition)
                {
                    ++duplicateIndex;
                }
                else
                {
                    RemoveWaypoint(duplicateIndex);
                    return true;
                }
            }

            return false;
        }

        public void RemoveWaypoint(int waypointIndex)
        {
            ConsumeWaypoint(waypointIndex);
            ++remainingWaypointsPlaceable.value;
        }

        public void RemoveLastWaypoint()
        {
            RemoveWaypoint(waypoints.Count - 1);
        }

        private void ConsumeWaypoint(int waypointIndex)
        {
            Waypoint waypoint = waypoints[waypointIndex];
            waypoint.OnRemoved();

            waypoints.RemoveAt(waypointIndex);
            --waypointsPlaced.value;
            onWaypointRemoved.Raise(waypoint.gridPosition);
        }

        #endregion

        #region Pathfinding

        private Stack<Vector3> CalculateGridSteps(Vector3Int startingPosition, Vector3Int targetPosition)
        {
            HashSet<Vector3Int> openSet = new HashSet<Vector3Int>() { startingPosition };
            Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
            Dictionary<Vector3Int, float> costFromStart = new Dictionary<Vector3Int, float>();
            Dictionary<Vector3Int, float> costOverall = new Dictionary<Vector3Int, float>();

            costFromStart.Add(startingPosition, 0);
            costOverall.Add(startingPosition, Math.Abs(targetPosition.x - startingPosition.x) + Math.Abs(targetPosition.y - startingPosition.y));

            while (openSet.Count > 0)
            {
                Vector3Int bestPosition = GetBestPosition(openSet, costOverall);
                if (bestPosition == targetPosition)
                {
                    return ConstructGridSteps(targetPosition, cameFrom);
                }

                openSet.Remove(bestPosition);

                // Left
                UpdateDirectional(new Vector3Int(-1, 0, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Up
                UpdateDirectional(new Vector3Int(0, 1, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Right
                UpdateDirectional(new Vector3Int(1, 0, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);

                // Down
                UpdateDirectional(new Vector3Int(0, -1, 0), bestPosition, targetPosition, openSet, cameFrom, costFromStart, costOverall);
            }

            return new Stack<Vector3>();
        }

        private Vector3Int GetBestPosition(HashSet<Vector3Int> openSet, Dictionary<Vector3Int, float> costOverall)
        {
            Vector3Int bestPosition = new Vector3Int();
            float currentCost = float.PositiveInfinity;

            foreach (var position in openSet)
            {
                if (costOverall.ContainsKey(position) && costOverall[position] < currentCost)
                {
                    bestPosition = position;
                    currentCost = costOverall[position];
                }
            }

            Debug.Assert(currentCost < float.PositiveInfinity, "Failed to find best cell for A*");
            return bestPosition;
        }

        private void UpdateDirectional(
            Vector3Int delta,
            Vector3Int bestPosition,
            Vector3Int targetPosition,
            HashSet<Vector3Int> openSet,
            Dictionary<Vector3Int, Vector3Int> cameFrom,
            Dictionary<Vector3Int, float> costFromStart,
            Dictionary<Vector3Int, float> costOverall)
        {
            Vector3Int neighbour = bestPosition + delta;
            if (movementTilemap.HasTile(neighbour) && !doorsTilemap.HasTile(neighbour))
            {
                const float distanceToNeighbour = 1;
                float tentativeCostFromStart = costFromStart[bestPosition] + distanceToNeighbour;
                float neighbourScore = costFromStart.ContainsKey(neighbour) ? costFromStart[neighbour] : float.MaxValue;

                if (tentativeCostFromStart < neighbourScore)
                {
                    if (!cameFrom.ContainsKey(neighbour))
                    {
                        cameFrom.Add(neighbour, bestPosition);
                    }
                    else
                    {
                        cameFrom[neighbour] = bestPosition;
                    }

                    if (!costFromStart.ContainsKey(neighbour))
                    {
                        costFromStart.Add(neighbour, tentativeCostFromStart);
                    }
                    else
                    {
                        costFromStart[neighbour] = tentativeCostFromStart;
                    }

                    if (!costOverall.ContainsKey(neighbour))
                    {
                        costOverall.Add(neighbour, costFromStart[neighbour] + Math.Abs(targetPosition.x - neighbour.x) + Math.Abs(targetPosition.y - neighbour.y));
                    }

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        private Stack<Vector3> ConstructGridSteps(Vector3Int targetGridPosition, Dictionary<Vector3Int, Vector3Int> cameFrom)
        {
            Stack<Vector3> newWaypoints = new Stack<Vector3>();

            while (cameFrom.ContainsKey(targetGridPosition))
            {
                newWaypoints.Push(grid.GetCellCenterWorld(targetGridPosition));
                targetGridPosition = cameFrom[targetGridPosition];
            }

            return newWaypoints;
        }

#endregion
    }
}
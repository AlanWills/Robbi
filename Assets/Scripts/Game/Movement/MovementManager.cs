using Robbi.Debugging.Logging;
using Robbi.Events;
using Robbi.Parameters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Manager")]
    public class MovementManager : MonoBehaviour
    {
        #region Waypoint

        private class Waypoint
        {
            public Vector3Int gridPosition;
            public Stack<Vector3> stepsFromPrevious;
            public GameObject destinationMarkerInstance;

            public Waypoint(
                Vector3Int gridPosition, 
                Stack<Vector3> stepsFromPrevious,
                GameObject destinationMarkerInstance)
            {
                this.gridPosition = gridPosition;
                this.stepsFromPrevious = stepsFromPrevious;
                this.destinationMarkerInstance = destinationMarkerInstance;
            }

            public void OnReached()
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
                GetComponent<TilemapRenderer>().enabled = value;
            }
        }

        [Header("Tilemaps")]
        public Tilemap movementTilemap;
        public Tilemap doorsTilemap;
        
        [Header("Parameters")]
        public Vector3Value playerLocalPosition;
        public Vector3IntEvent onMovedTo;
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;

        [Header("Other")]
        public GameObject destinationMarkerPrefab;
        public float speed = 1;

        private Grid grid;
        private Stack<Waypoint> waypoints = new Stack<Waypoint>();
        private bool isMoving = false;

        #endregion

        #region Unity Methods

        private void Start()
        {
            grid = movementTilemap.layoutGrid;
        }

        private void Update()
        {
            if (isMoving)
            {
                Waypoint currentWaypoint = waypoints.Peek();
                Vector3 nextStepPosition = currentWaypoint.stepsFromPrevious.Peek();
                Vector3 newPosition = Vector3.MoveTowards(playerLocalPosition.value, nextStepPosition, speed * Time.deltaTime);

                if (newPosition == playerLocalPosition.value)
                {
                    Vector3Int movedTo = new Vector3Int(Mathf.FloorToInt(newPosition.x), Mathf.FloorToInt(newPosition.y), Mathf.FloorToInt(newPosition.z));
                    onMovedTo.Raise(movedTo);

                    // This step of movement is completed
                    currentWaypoint.stepsFromPrevious.Pop();

                    if (currentWaypoint.stepsFromPrevious.Count == 0)
                    {
                        // We have completed all the steps required to get to the waypoint
                        ConsumeLastWaypoint();
                    }

                    isMoving = waypoints.Count > 0;
                }
                else
                {
                    playerLocalPosition.value = newPosition;
                }
            }
        }

        #endregion

        #region Movement Methods

        public void ExecuteMove()
        {
            isMoving = true;

            Stack<Waypoint> reverseStack = new Stack<Waypoint>(waypoints.Count);
            while (waypoints.Count > 0)
            {
                reverseStack.Push(waypoints.Pop());
            }

            waypoints = reverseStack;
        }

        public void AddWaypoint(Vector3 waypointWorldPosition)
        {
            if (isMoving)
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

            Vector3Int lastWaypointGridPosition = waypoints.Count != 0 ? waypoints.Peek().gridPosition : grid.WorldToCell(playerLocalPosition.value); ;

            if (waypointGridPosition != lastWaypointGridPosition && movementTilemap.HasTile(waypointGridPosition))
            {
                Stack<Vector3> gridSteps = CalculateGridSteps(lastWaypointGridPosition, waypointGridPosition);
                if (gridSteps.Count > 0)
                {
                    GameObject destinationMarkerInstance = GameObject.Instantiate(destinationMarkerPrefab, movementTilemap.transform);
                    destinationMarkerInstance.transform.position = grid.GetCellCenterLocal(waypointGridPosition);
                    
                    waypoints.Push(new Waypoint(waypointGridPosition, gridSteps, destinationMarkerInstance));
                    --remainingWaypointsPlaceable.value;
                    ++waypointsPlaced.value;
                }
                else
                {
                    HudLogger.LogWarning(string.Format("Invalid location {0} for movement", waypointGridPosition));
                }
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
                    break;
                }
            }

            if (duplicateIndex == waypoints.Count)
            {
                return false;
            }

            for (int i = 0; i <= duplicateIndex; ++i)
            {
                UndoLastWaypoint();
            }

            return true;
        }

        public void UndoLastWaypoint()
        {
            ConsumeLastWaypoint();
            ++remainingWaypointsPlaceable.value;
        }

        private void ConsumeLastWaypoint()
        {
            Waypoint waypoint = waypoints.Pop();
            waypoint.OnReached();
            --waypointsPlaced.value;
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
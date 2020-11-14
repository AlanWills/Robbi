﻿using Robbi.Debugging.Logging;
using Robbi.Events;
using Robbi.Memory;
using Robbi.Parameters;
using Robbi.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

using Event = Robbi.Events.Event;
using Robbi.Managers;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Manager")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class MovementManager : NamedManager
    {
        #region Waypoint

        public class Waypoint
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
                destinationMarkerInstance.SetActive(false);
            }
        }

        #endregion

        #region Properties and Fields

        public int NumWaypoints
        {
            get { return waypoints.Count; }
        }

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;
        public TilemapValue doorsTilemap;
        
        [Header("Events")]
        public Vector3Value playerLocalPosition;
        public Vector3IntEvent onMovedTo;
        public Vector3IntEvent onMovedFrom;
        public Vector3IntEvent onWaypointPlaced;
        public Vector3IntEvent onWaypointRemoved;
        public Event levelLoseWaypointUnreachable;
        public Event levelLoseOutOfWaypoints;

        [Header("Parameters")]
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;
        public BoolValue isProgramRunning;

        [Header("Other")]
        public GameObjectAllocator destinationMarkerAllocator;
        public FloatValue movementSpeed;
        public BoxCollider2D boundingBox;

        private List<Waypoint> waypoints = new List<Waypoint>();
        private Stack<Vector3> stepsToNextWaypoint = new Stack<Vector3>();

        // Temporary structs for A*
        private HashSet<Vector3Int> openSet = new HashSet<Vector3Int>();
        private Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        private Dictionary<Vector3Int, float> costFromStart = new Dictionary<Vector3Int, float>();
        private Dictionary<Vector3Int, float> costOverall = new Dictionary<Vector3Int, float>();
        private List<Vector3Int> newWaypoints = new List<Vector3Int>();

        #endregion

        #region Unity Methods

        private void Start()
        {
            waypointsPlaced.value = 0;
            isProgramRunning.value = false;
            movementSpeed.value = OptionsManager.Instance.DefaultMovementSpeed;

            Vector3Int movementGridSize = movementTilemap.value.size;
            Vector3Int movementOrigin = movementTilemap.value.origin;
            boundingBox.size = new Vector2(movementGridSize.x, movementGridSize.y);
            boundingBox.offset = new Vector2(movementOrigin.x + movementGridSize.x * 0.5f, movementOrigin.y + movementGridSize.y * 0.5f);
        }

        private void LateUpdate()
        {
            if (isProgramRunning.value)
            {
                Vector3 playerLocalPos = playerLocalPosition.value;
                Vector3Int movedFrom = new Vector3Int(Mathf.RoundToInt(playerLocalPos.x - 0.5f), Mathf.RoundToInt(playerLocalPos.y - 0.5f), Mathf.RoundToInt(playerLocalPos.z));

                if (stepsToNextWaypoint.Count > 0)
                {
                    // We are moving towards our next waypoint along the steps
                    Vector3 nextStepPosition = stepsToNextWaypoint.Peek();
                    Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.value * Time.deltaTime);
                    Vector3Int movedTo = new Vector3Int(Mathf.RoundToInt(newPosition.x - 0.5f), Mathf.RoundToInt(newPosition.y - 0.5f), Mathf.RoundToInt(newPosition.z));
                    
                    if (newPosition == playerLocalPosition.value)
                    {
                        onMovedTo.Raise(movedTo);

                        // This step of movement is completed
                        stepsToNextWaypoint.Pop();
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
                    if (movedFrom == waypoints[0].gridPosition)
                    {
                        // We have reached the next waypoint
                        ConsumeWaypoint(0);
                        MoveToNextWaypoint();
                    }
                    else
                    {
                        levelLoseWaypointUnreachable.Raise();
                        isProgramRunning.value = false;
                    }
                }
            }
        }

        private void OnValidate()
        {
            if (destinationMarkerAllocator == null)
            {
                destinationMarkerAllocator = GetComponent<GameObjectAllocator>();
            }

            if (boundingBox == null)
            {
                boundingBox = GetComponent<BoxCollider2D>();
            }
        }

        #endregion

        #region Game Lost Methods

        public void CheckForLevelLoseOutOfWaypoints()
        {
            if (waypointsPlaced.value == 0 && remainingWaypointsPlaceable.value == 0)
            {
                levelLoseOutOfWaypoints.Raise();
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            isProgramRunning.value = waypoints.Count > 0;
            
            if (isProgramRunning.value)
            {
                CalculateGridSteps(movementTilemap.value.LocalToCell(playerLocalPosition.value), waypoints[0].gridPosition);
            }
        }

        public void AddWaypoint(Vector3 waypointWorldPosition)
        {
            if (isProgramRunning.value)
            {
                // Cannot add waypoints whilst movement program is running
                return;
            }

            Vector3Int waypointGridPosition = movementTilemap.value.WorldToCell(waypointWorldPosition);
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

            Vector3Int lastWaypointGridPosition = waypoints.Count != 0 ? waypoints[waypoints.Count - 1].gridPosition : movementTilemap.value.WorldToCell(playerLocalPosition.value); ;

            if (waypointGridPosition != lastWaypointGridPosition && movementTilemap.value.HasTile(waypointGridPosition))
            {
                if (!destinationMarkerAllocator.CanAllocate(1))
                {
                    destinationMarkerAllocator.AddChunk();
                }

                GameObject destinationMarkerInstance = destinationMarkerAllocator.Allocate();
                destinationMarkerInstance.transform.position = movementTilemap.value.GetCellCenterLocal(waypointGridPosition);

                waypoints.Add(new Waypoint(waypointGridPosition, destinationMarkerInstance));
                --remainingWaypointsPlaceable.value;
                ++waypointsPlaced.value;
                onWaypointPlaced.Raise(waypointGridPosition);

                HudLogger.LogInfoFormat("Waypoint added at {0}", waypointGridPosition);
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

        private void CalculateGridSteps(Vector3Int startingPosition, Vector3Int targetPosition)
        {
            openSet.Clear();
            cameFrom.Clear();
            costFromStart.Clear();
            costOverall.Clear();

            openSet.Add(startingPosition);
            costFromStart.Add(startingPosition, 0);
            costOverall.Add(startingPosition, Math.Abs(targetPosition.x - startingPosition.x) + Math.Abs(targetPosition.y - startingPosition.y));

            while (openSet.Count > 0)
            {
                Vector3Int bestPosition = GetBestPosition(openSet, costOverall);
                if (bestPosition == targetPosition)
                {
                    ConstructGridSteps(targetPosition);
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

            ConstructGridSteps(targetPosition);
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
            if (movementTilemap.value.HasTile(neighbour))
            {
                float distanceToNeighbour = doorsTilemap.value.HasTile(neighbour) ? 1000.0f : 1.0f;
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

        private void ConstructGridSteps(Vector3Int targetGridPosition)
        {
            newWaypoints.Clear();
            stepsToNextWaypoint.Clear();

            while (cameFrom.ContainsKey(targetGridPosition))
            {
                newWaypoints.Add(targetGridPosition);
                targetGridPosition = cameFrom[targetGridPosition];
            }

            for (int i = newWaypoints.Count - 1; i >= 0; --i)
            {
                if (doorsTilemap.value.HasTile(newWaypoints[i]))
                {
                    for (int j = i; j >= 0; --j)
                    {
                        newWaypoints.RemoveAt(j);
                    }

                    break;
                }
            }

            foreach (Vector3Int waypoint in newWaypoints)
            {
                stepsToNextWaypoint.Push(movementTilemap.value.GetCellCenterWorld(waypoint));
            }
        }

        #endregion

        #region Waypoint Access

        public Waypoint GetWaypoint(int waypointIndex)
        {
            return 0 <= waypointIndex && waypointIndex < NumWaypoints ? waypoints[waypointIndex] : null;
        }

        #endregion

        #region Speed

        public void DoubleSpeed()
        {

        }

        #endregion
    }
}
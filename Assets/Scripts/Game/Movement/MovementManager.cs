using Robbi.Debugging.Logging;
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
        public Event onInvalidWaypointPlaced;
        public Event levelLoseWaypointUnreachable;
        public Event levelLoseOutOfWaypoints;
        public Event levelLoseOutOfFuel;

        [Header("Parameters")]
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;
        public BoolValue isProgramRunning;
        public BoolValue levelRequiresFuel;
        public UIntValue remainingFuel;

        [Header("Other")]
        public GameObjectAllocator destinationMarkerAllocator;
        public FloatValue movementSpeed;
        public BoxCollider2D boundingBox;

        private List<Waypoint> waypoints = new List<Waypoint>();
        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region Unity Methods

        private void Start()
        {
            waypointsPlaced.value = 0;
            isProgramRunning.value = false;
            movementSpeed.value = OptionsManager.Instance.DefaultMovementSpeed;
            aStarMovement.MovementTilemap = movementTilemap.value;
            aStarMovement.DoorsTilemap = doorsTilemap.value;

            Vector3Int movementGridSize = movementTilemap.value.size;
            Vector3Int movementOrigin = movementTilemap.value.origin;
            boundingBox.size = new Vector2(movementGridSize.x, movementGridSize.y);
            boundingBox.offset = new Vector2(movementOrigin.x + movementGridSize.x * 0.5f, movementOrigin.y + movementGridSize.y * 0.5f);
        }

        private void Update()
        {
            if (isProgramRunning.value)
            {
                Vector3 playerLocalPos = playerLocalPosition.value;
                Vector3Int movedFrom = new Vector3Int(Mathf.RoundToInt(playerLocalPos.x - 0.5f), Mathf.RoundToInt(playerLocalPos.y - 0.5f), Mathf.RoundToInt(playerLocalPos.z));

                if (remainingFuel.value == 0)
                {
                    // Don't immediately raise this when we have bingo fuel
                    // We might have moved onto a tile with a fuel pickup on it
                    // Instead wait for a new frame to see if we have no fuel
                    levelLoseOutOfFuel.Raise();
                }
                else if (aStarMovement.HasStepsToNextWaypoint)
                {
                    // We are moving towards our next waypoint along the steps
                    Vector3 nextStepPosition = aStarMovement.NextStep;
                    Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.value * Time.deltaTime);
                    Vector3Int movedTo = new Vector3Int(Mathf.RoundToInt(newPosition.x - 0.5f), Mathf.RoundToInt(newPosition.y - 0.5f), Mathf.RoundToInt(newPosition.z));
                    
                    if (newPosition == nextStepPosition)
                    {
                        // This step of movement is completed
                        aStarMovement.CompleteStep();
                        
                        // Pay fuel costs
                        RemoveFuel(1);

                        if (movedTo == waypoints[0].gridPosition)
                        {
                            // We have reached the next waypoint
                            ConsumeWaypoint(0);
                            MoveToNextWaypoint();
                        }
                        
                        onMovedTo.Raise(movedTo);
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
                    levelLoseWaypointUnreachable.Raise();
                    isProgramRunning.value = false;
                }
            }
            else if (waypoints.Count == 0 && remainingWaypointsPlaceable.value == 0)
            {
                levelLoseOutOfWaypoints.Raise();
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

        #region Callbacks

        public void OnLevelChanged()
        {
            MoveToNextWaypoint();
        }

        #endregion

        #region Fuel Methods

        public void AddFuel(uint amount)
        {
            if (levelRequiresFuel.value)
            {
                remainingFuel.value += amount;
            }
        }

        public void RemoveFuel(uint amount)
        {
            if (levelRequiresFuel.value)
            {
                // Make sure we don't go below 0 fuel otherwise we'll wrap around
                remainingFuel.value -= Math.Min(amount, remainingFuel.value);
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            isProgramRunning.value = waypoints.Count > 0;
            
            if (isProgramRunning.value)
            {
                aStarMovement.CalculateGridSteps(playerLocalPosition.value, waypoints[0].gridPosition);
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
                onInvalidWaypointPlaced.Raise();
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
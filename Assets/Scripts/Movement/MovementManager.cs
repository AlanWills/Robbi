using Celeste.Events;
using Celeste.Parameters;
using Robbi.Options;
using System;
using System.Collections.Generic;
using UnityEngine;

using Event = Celeste.Events.Event;
using Celeste.Log;
using Celeste.Managers;
using Celeste.Tilemaps;
using Celeste.Memory;

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
            waypointsPlaced.Value = 0;
            isProgramRunning.Value = false;
            movementSpeed.Value = OptionsManager.Instance.DefaultMovementSpeed;
            aStarMovement.MovementTilemap = movementTilemap.Value;
            aStarMovement.DoorsTilemap = doorsTilemap.Value;

            Vector3Int movementGridSize = movementTilemap.Value.size;
            Vector3Int movementOrigin = movementTilemap.Value.origin;
            boundingBox.size = new Vector2(movementGridSize.x, movementGridSize.y);
            boundingBox.offset = new Vector2(movementOrigin.x + movementGridSize.x * 0.5f, movementOrigin.y + movementGridSize.y * 0.5f);
        }

        private void Update()
        {
            if (isProgramRunning.Value)
            {
                Vector3 playerLocalPos = playerLocalPosition.Value;
                Vector3Int movedFrom = new Vector3Int(Mathf.RoundToInt(playerLocalPos.x - 0.5f), Mathf.RoundToInt(playerLocalPos.y - 0.5f), Mathf.RoundToInt(playerLocalPos.z));

                if (levelRequiresFuel.Value && remainingFuel.Value == 0)
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
                    Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.Value * Time.deltaTime);
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
                        playerLocalPosition.Value = newPosition;

                        if (movedFrom != movedTo)
                        {
                            onMovedFrom.Raise(movedFrom);
                        }
                    }
                }
                else
                {
                    levelLoseWaypointUnreachable.Raise();
                    isProgramRunning.Value = false;
                }
            }
            else if (waypoints.Count == 0 && remainingWaypointsPlaceable.Value == 0)
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

        private void OnDrawGizmos()
        {
            if (waypoints.Count == 0)
            {
                return;
            }

            Vector3 currentPosition = playerLocalPosition.Value;
            foreach (Vector3 position in aStarMovement.CalculateGridStepsWithoutCaching(currentPosition, waypoints[0].gridPosition))
            {
                Gizmos.DrawLine(currentPosition, position);
                currentPosition = position;
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
            if (levelRequiresFuel.Value)
            {
                remainingFuel.Value += amount;
            }
        }

        public void RemoveFuel(uint amount)
        {
            if (levelRequiresFuel.Value)
            {
                // Make sure we don't go below 0 fuel otherwise we'll wrap around
                remainingFuel.Value -= Math.Min(amount, remainingFuel.Value);
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            isProgramRunning.Value = waypoints.Count > 0;
            
            if (isProgramRunning.Value)
            {
                aStarMovement.CalculateGridSteps(playerLocalPosition.Value, waypoints[0].gridPosition);
            }
        }

        public void AddWaypoint(Vector3 waypointWorldPosition)
        {
            if (isProgramRunning.Value)
            {
                // Cannot add waypoints whilst movement program is running
                return;
            }

            Vector3Int waypointGridPosition = movementTilemap.Value.WorldToCell(waypointWorldPosition);
            if (WaypointExists(waypointGridPosition))
            {
                // Cannot add waypoints to a position that already has one
                return;
            }

            if (remainingWaypointsPlaceable.Value <= 0)
            {
                // Cannot add waypoints if we have run out of our allotted amount
                onInvalidWaypointPlaced.Raise();
                return;
            }

            Vector3Int lastWaypointGridPosition = waypoints.Count != 0 ? waypoints[waypoints.Count - 1].gridPosition : movementTilemap.Value.WorldToCell(playerLocalPosition.Value); ;

            if (waypointGridPosition != lastWaypointGridPosition && movementTilemap.Value.HasTile(waypointGridPosition))
            {
                if (!destinationMarkerAllocator.CanAllocate(1))
                {
                    destinationMarkerAllocator.AddChunk();
                }

                GameObject destinationMarkerInstance = destinationMarkerAllocator.Allocate();
                destinationMarkerInstance.transform.position = movementTilemap.Value.GetCellCenterLocal(waypointGridPosition);

                waypoints.Add(new Waypoint(waypointGridPosition, destinationMarkerInstance));
                --remainingWaypointsPlaceable.Value;
                ++waypointsPlaced.Value;
                onWaypointPlaced.Raise(waypointGridPosition);

                HudLog.LogInfoFormat("Waypoint added at {0}", waypointGridPosition);
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
            ++remainingWaypointsPlaceable.Value;
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
            --waypointsPlaced.Value;
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
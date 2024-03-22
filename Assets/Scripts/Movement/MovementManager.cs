using Celeste.Events;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using UnityEngine;
using Event = Celeste.Events.Event;
using Celeste.Log;
using Celeste.Tilemaps;
using Celeste.Memory;
using Celeste.Options;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Manager")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class MovementManager : MonoBehaviour
    {
        #region Waypoint

        public class Waypoint
        {
            public Vector3Int gridPosition;
            public WaypointMarker destinationMarker;

            public Waypoint(Vector3Int gridPosition, WaypointMarker destinationMarker, int index)
            {
                this.gridPosition = gridPosition;
                this.destinationMarker = destinationMarker;

                SetWaypointNumber(index);
            }

            public void OnRemoved()
            {
                destinationMarker.gameObject.SetActive(false);
            }

            public void SetWaypointNumber(int index)
            {
                destinationMarker.SetWaypointNumber(index);
            }
        }

        #endregion

        #region Properties and Fields

        public int NumWaypoints => waypoints.Count;

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
        public StringEvent levelLose;

        [Header("Parameters")]
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;
        public BoolValue isProgramRunning;
        public BoolValue levelRequiresFuel;
        public UIntValue remainingFuel;
        public StringValue waypointUnreachableReason;
        public StringValue outOfWaypointsReason;
        public StringValue outOfFuelReason;
        [SerializeField] private IntValue defaultMovementSpeed;

        [Header("Other")]
        public GameObjectAllocator destinationMarkerAllocator;
        public FloatValue movementSpeed;
        public BoxCollider2D boundingBox;
        [SerializeField] private OptionsRecord optionsRecord;

        private List<Waypoint> waypoints = new List<Waypoint>();
        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region IEnvironmentManager

        public void Initialize()
        {
            waypointsPlaced.Value = 0;
            isProgramRunning.Value = false;
            movementSpeed.Value = defaultMovementSpeed.Value;
            aStarMovement.MovementTilemap = movementTilemap.Value;
            aStarMovement.DoorsTilemap = doorsTilemap.Value;

            Vector3Int movementGridSize = movementTilemap.Value.size;
            Vector3Int movementOrigin = movementTilemap.Value.origin;
            boundingBox.size = new Vector2(movementGridSize.x, movementGridSize.y);
            boundingBox.offset = new Vector2(movementOrigin.x + movementGridSize.x * 0.5f, movementOrigin.y + movementGridSize.y * 0.5f);
        }

        public void Cleanup()
        {
            waypoints.Clear();
            destinationMarkerAllocator.DeallocateAll();

            aStarMovement.MovementTilemap = null;
            aStarMovement.DoorsTilemap = null;
        }

        #endregion

        #region Unity Methods

        private void FixedUpdate()
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
                    levelLose.Invoke(outOfFuelReason.Value);
                }
                else if (aStarMovement.HasStepsToNextWaypoint)
                {
                    // We are moving towards our next waypoint along the steps
                    Vector3 nextStepPosition = aStarMovement.NextStep;
                    Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.Value * Time.deltaTime);
                    Vector3Int movedTo = new Vector3Int(
                        Mathf.RoundToInt(newPosition.x - 0.5f), 
                        Mathf.RoundToInt(newPosition.y - 0.5f), 
                        Mathf.RoundToInt(newPosition.z));
                    playerLocalPosition.Value = newPosition;
                    
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
                        
                        onMovedTo.Invoke(movedTo);
                    }
                    else
                    {
                        if (movedFrom != movedTo)
                        {
                            onMovedFrom.Invoke(movedFrom);
                        }
                    }
                }
                else
                {
                    levelLose.Invoke(waypointUnreachableReason.Value);
                    isProgramRunning.Value = false;
                }
            }
            else if (waypoints.Count == 0 && remainingWaypointsPlaceable.Value == 0)
            {
                levelLose.Invoke(outOfWaypointsReason.Value);
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

        public void OnPortalExited(Vector3Int position)
        {
            MoveToNextWaypoint();
        }

        public void OnBoosterStart()
        {
            boundingBox.enabled = false;
        }

        public void OnBoosterFinish()
        {
            boundingBox.enabled = true;
        }

        public void OnAddWaypoints(uint amount)
        {
            remainingWaypointsPlaceable.Value += (int)amount;
        }

        public void OnRemoveWaypoints(uint amount)
        {
            remainingWaypointsPlaceable.Value = remainingWaypointsPlaceable.Value >= (int)amount ? remainingWaypointsPlaceable.Value - (int)amount : 0;
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

        #endregion

        #region Waypoint Management

        private void UpdateWaypointNumbers()
        {
            if (!isProgramRunning.Value)
            {
                for (int i = 0; i < waypoints.Count; ++i)
                {
                    waypoints[i].SetWaypointNumber(i + 1);
                }
            }
        }

        public Waypoint GetWaypoint(int waypointIndex)
        {
            return 0 <= waypointIndex && waypointIndex < NumWaypoints ? waypoints[waypointIndex] : null;
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
                onInvalidWaypointPlaced.Invoke();
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

                waypoints.Add(new Waypoint(waypointGridPosition, destinationMarkerInstance.GetComponent<WaypointMarker>(), waypoints.Count + 1));
                --remainingWaypointsPlaceable.Value;
                ++waypointsPlaced.Value;
                onWaypointPlaced.Invoke(waypointGridPosition);

                HudLog.LogInfo($"Waypoint added at {waypointGridPosition}");
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
            onWaypointRemoved.Invoke(waypoint.gridPosition);

            UpdateWaypointNumbers();
        }

        #endregion
    }
}
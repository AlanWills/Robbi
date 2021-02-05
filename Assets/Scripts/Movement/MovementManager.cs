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
using Robbi.Runtime.Actors;
using Robbi.Events.Runtime.Actors;

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

        public int NumWaypoints
        {
            get { return waypoints.Count; }
        }

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;
        public TilemapValue doorsTilemap;
        public TilemapValue portalsTilemap;

        [Header("Events")]
        public CharacterRuntimeEvent onCharacterMovedTo;
        public Vector3IntEvent onMovedFrom;
        public Vector3IntEvent onWaypointPlaced;
        public Vector3IntEvent onWaypointRemoved;
        public Event onInvalidWaypointPlaced;

        [Header("Parameters")]
        public IntValue remainingWaypointsPlaceable;
        public IntValue waypointsPlaced;
        public BoolValue isProgramRunning;
        public BoolValue nextWaypointUnreachable;

        [Header("Other")]
        public GameObjectAllocator destinationMarkerAllocator;
        public FloatValue movementSpeed;
        public BoxCollider2D boundingBox;

        private CharacterRuntime characterRuntime;
        private List<Waypoint> waypoints = new List<Waypoint>();
        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        public void Initialize(CharacterRuntime _characterRuntime)
        {
            characterRuntime = _characterRuntime;
            waypointsPlaced.Value = 0;
            isProgramRunning.Value = false;
            nextWaypointUnreachable.Value = false;
            movementSpeed.Value = OptionsManager.Instance.DefaultMovementSpeed;
            aStarMovement.MovementTilemap = movementTilemap.Value;
            aStarMovement.DoorsTilemap = doorsTilemap.Value;
            aStarMovement.PortalsTilemap = portalsTilemap.Value;
            aStarMovement.PortalsWeight = 0;    // Add no extra cost for a tile with a portal

            Vector3Int movementGridSize = movementTilemap.Value.size;
            Vector3Int movementOrigin = movementTilemap.Value.origin;
            boundingBox.size = new Vector2(movementGridSize.x, movementGridSize.y);
            boundingBox.offset = new Vector2(movementOrigin.x + movementGridSize.x * 0.5f, movementOrigin.y + movementGridSize.y * 0.5f);
        }

        public void Cleanup()
        {
            waypoints.Clear();
            destinationMarkerAllocator.DeallocateAll();
            isProgramRunning.Value = false;

            characterRuntime = null;
            aStarMovement.MovementTilemap = null;
            aStarMovement.DoorsTilemap = null;
        }

        #region Unity Methods

        private void FixedUpdate()
        {
            if (!isProgramRunning.Value)
            {
                return;
            }

            isProgramRunning.Value = aStarMovement.HasStepsToNextWaypoint;
            Vector3 playerOriginalPosition = characterRuntime.Position;

            if (aStarMovement.HasStepsToNextWaypoint)
            {
                // We are moving towards our next waypoint along the steps
                Vector3 nextStepPosition = aStarMovement.NextStep;
                Vector3 newPosition = Vector3.MoveTowards(playerOriginalPosition, nextStepPosition, movementSpeed.Value * Time.deltaTime);
                Vector3Int movedTo = new Vector3Int(
                    Mathf.RoundToInt(newPosition.x - 0.5f),
                    Mathf.RoundToInt(newPosition.y - 0.5f),
                    Mathf.RoundToInt(newPosition.z));
                characterRuntime.Position = newPosition;

                if (newPosition == nextStepPosition)
                {
                    // This step of movement is completed
                    aStarMovement.CompleteStep();
                    nextWaypointUnreachable.Value = !aStarMovement.HasStepsToNextWaypoint;

                    if (movedTo == waypoints[0].gridPosition)
                    {
                        // We have reached the next waypoint
                        ConsumeWaypoint(0);
                        MoveToNextWaypoint();
                    }

                    Debug.LogFormat("{0} moved to {1}", characterRuntime.name, characterRuntime.Tile);
                    onCharacterMovedTo.RaiseSilently(characterRuntime);
                }
                else
                {
                    Vector3Int movedFrom = new Vector3Int(
                        Mathf.RoundToInt(playerOriginalPosition.x - 0.5f), 
                        Mathf.RoundToInt(playerOriginalPosition.y - 0.5f), 
                        Mathf.RoundToInt(playerOriginalPosition.z));
                    
                    if (movedFrom != movedTo)
                    {
                        onMovedFrom.Raise(movedFrom);
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

        private void OnDrawGizmos()
        {
            if (waypoints.Count == 0)
            {
                return;
            }

            Vector3 currentPosition = characterRuntime.Position;
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

        public void OnPortalExited(CharacterRuntime characterRuntime)
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

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            isProgramRunning.Value = waypoints.Count > 0;
            
            if (isProgramRunning.Value)
            {
                aStarMovement.CalculateGridSteps(characterRuntime.Position, waypoints[0].gridPosition);
                nextWaypointUnreachable.Value = !aStarMovement.HasStepsToNextWaypoint;
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
                onInvalidWaypointPlaced.Raise();
                return;
            }

            Vector3Int lastWaypointGridPosition = waypoints.Count != 0 ? waypoints[waypoints.Count - 1].gridPosition : characterRuntime.Tile;

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

            UpdateWaypointNumbers();
        }

        #endregion
    }
}
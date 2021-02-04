using Celeste.Events;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Events.Runtime.Actors;
using Robbi.Runtime.Actors;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Targeted Movement")]
    [RequireComponent(typeof(CharacterRuntime))]
    public class TargetedMovement : MonoBehaviour
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;
        public TilemapValue doorsTilemap;

        [Header("Events")]
        public CharacterRuntimeEvent onCharacterMovedTo;
        public Vector3IntEvent onMovedFrom;

        [Header("Other")]
        public Vector3Value targetPosition;
        public BoolValue shouldMove;
        public FloatValue movementSpeed;
        public CharacterRuntime characterRuntime;
        
        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (characterRuntime == null)
            {
                characterRuntime = GetComponent<CharacterRuntime>();
            }
        }

        private void Start()
        {
            aStarMovement.DoorsTilemap = doorsTilemap.Value;
            aStarMovement.MovementTilemap = movementTilemap.Value;
            
            MoveToNextWaypoint();
        }

        private void FixedUpdate()
        {
            if (!shouldMove.Value)
            {
                return;
            }

            Vector3 playerLocalPos = characterRuntime.Position;
            if (aStarMovement.HasStepsToNextWaypoint)
            {
                // We are moving towards our next waypoint along the steps
                Vector3 nextStepPosition = aStarMovement.NextStep;
                Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.Value * Time.deltaTime);
                Vector3Int movedTo = new Vector3Int(
                        Mathf.RoundToInt(newPosition.x - 0.5f),
                        Mathf.RoundToInt(newPosition.y - 0.5f),
                        Mathf.RoundToInt(newPosition.z));
                characterRuntime.Position = newPosition;

                if (newPosition == nextStepPosition)
                {
                    // This step of movement is completed
                    aStarMovement.CompleteStep();

                    Debug.LogFormat("{0} moved to {1}", characterRuntime.name, characterRuntime.Tile);
                    onCharacterMovedTo.RaiseSilently(characterRuntime);
                }
                else
                {
                    Vector3Int movedFrom = new Vector3Int(
                        Mathf.RoundToInt(playerLocalPos.x - 0.5f), 
                        Mathf.RoundToInt(playerLocalPos.y - 0.5f), 
                        Mathf.RoundToInt(playerLocalPos.z));
                    
                    if (movedFrom != movedTo)
                    {
                        onMovedFrom.Raise(movedFrom);
                    }
                }
            }
            else
            {
                MoveToNextWaypoint();
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            aStarMovement.CalculateGridSteps(characterRuntime.Position, movementTilemap.Value.WorldToCell(targetPosition.Value));
        }

        #endregion

        #region Callbacks

        public void OnLevelChanged()
        {
            MoveToNextWaypoint();
        }

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            MoveToNextWaypoint();
        }

        public void OnPortalExited(CharacterRuntime characterRuntime)
        {
            MoveToNextWaypoint();
        }

        #endregion
    }
}
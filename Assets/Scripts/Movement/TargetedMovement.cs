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

        private void Update()
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
                characterRuntime.Position = newPosition;

                if (newPosition == nextStepPosition)
                {
                    // This step of movement is completed
                    aStarMovement.CompleteStep();
                    onCharacterMovedTo.Raise(characterRuntime);
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
            aStarMovement.CalculateGridSteps(characterRuntime.Tile, movementTilemap.Value.WorldToCell(targetPosition.Value));
        }

        #endregion

        #region Callbacks

        public void OnLevelChanged()
        {
            MoveToNextWaypoint();
        }

        public void OnMovedTo(CharacterRuntime characterRuntime)
        {
            MoveToNextWaypoint();
        }

        #endregion
    }
}
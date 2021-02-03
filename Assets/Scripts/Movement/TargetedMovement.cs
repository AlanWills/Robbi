using Celeste.Parameters;
using Celeste.Tilemaps;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Targeted Movement")]
    public class TargetedMovement : MonoBehaviour
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        public TilemapValue movementTilemap;
        public TilemapValue doorsTilemap;
        
        [Header("Other")]
        public Vector3Value targetPosition;
        public BoolValue shouldMove;
        public FloatValue movementSpeed;

        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region Unity Methods

        private void Start()
        {
            aStarMovement.DoorsTilemap = doorsTilemap.Value;
            aStarMovement.MovementTilemap = movementTilemap.Value;
            aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.Value.WorldToCell(targetPosition.Value));
        }

        private void Update()
        {
            if (!shouldMove.Value)
            {
                return;
            }

            Vector3 playerLocalPos = transform.localPosition;

            if (aStarMovement.HasStepsToNextWaypoint)
            {
                // We are moving towards our next waypoint along the steps
                Vector3 nextStepPosition = aStarMovement.NextStep;
                Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed.Value * Time.deltaTime);

                if (newPosition == nextStepPosition)
                {
                    // This step of movement is completed
                    aStarMovement.CompleteStep();
                }
                else
                {
                    transform.localPosition = newPosition;
                }
            }
            else
            {
                aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.Value.WorldToCell(targetPosition.Value));
            }
        }

        #endregion

        #region Movement Methods

        public void MoveToNextWaypoint()
        {
            aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.Value.WorldToCell(targetPosition.Value));
        }

        #endregion

        #region Callbacks

        public void OnLevelChanged()
        {
            MoveToNextWaypoint();
        }

        public void OnMovedTo()
        {
            aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.Value.WorldToCell(targetPosition.Value));
        }

        #endregion
    }
}
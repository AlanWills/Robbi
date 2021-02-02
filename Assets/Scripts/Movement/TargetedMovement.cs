using Celeste.Parameters;
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
        public Tilemap movementTilemap;
        public Tilemap doorsTilemap;
        
        [Header("Other")]
        public Vector3Value targetPosition;
        public BoolValue shouldMove;
        public FloatValue movementSpeed;

        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region Unity Methods

        private void Start()
        {
            aStarMovement.DoorsTilemap = doorsTilemap;
            aStarMovement.MovementTilemap = movementTilemap;
            aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.WorldToCell(targetPosition.Value));
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
                aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.WorldToCell(targetPosition.Value));
            }
        }

        #endregion

        #region Callbacks

        public void OnMovedTo()
        {
            aStarMovement.CalculateGridSteps(transform.localPosition, movementTilemap.WorldToCell(targetPosition.Value));
        }

        #endregion
    }
}
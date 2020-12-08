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

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Automated Movement")]
    public class AutomatedMovement : MonoBehaviour
    {
        #region Properties and Fields

        public int NumWaypoints
        {
            get { return waypoints.Count; }
        }

        [Header("Tilemaps")]
        public Tilemap movementTilemap;
        public Tilemap doorsTilemap;
        
        [Header("Other")]
        public Vector3Value robbiPosition;
        public Vector3Int startingPosition;
        public float movementSpeed = 4;

        [SerializeField]
        private List<Vector3Int> waypoints = new List<Vector3Int>();

        private AStarMovement aStarMovement = new AStarMovement();

        #endregion

        #region Unity Methods

        private void Start()
        {
            robbiPosition.Value = movementTilemap.layoutGrid.GetCellCenterLocal(startingPosition);

            aStarMovement.DoorsTilemap = doorsTilemap;
            aStarMovement.MovementTilemap = movementTilemap;
            aStarMovement.CalculateGridSteps(robbiPosition.Value, waypoints[0]);
        }

        private void Update()
        {
            Vector3 playerLocalPos = robbiPosition.Value;

            if (aStarMovement.HasStepsToNextWaypoint)
            {
                // We are moving towards our next waypoint along the steps
                Vector3 nextStepPosition = aStarMovement.NextStep;
                Vector3 newPosition = Vector3.MoveTowards(playerLocalPos, nextStepPosition, movementSpeed * Time.deltaTime);
                Vector3Int movedTo = new Vector3Int(Mathf.RoundToInt(newPosition.x - 0.5f), Mathf.RoundToInt(newPosition.y - 0.5f), Mathf.RoundToInt(newPosition.z));

                if (newPosition == nextStepPosition)
                {
                    // This step of movement is completed
                    aStarMovement.CompleteStep();

                    if (movedTo == waypoints[0])
                    {
                        waypoints.RemoveAt(0);
                    }
                }
                else
                {
                    robbiPosition.Value = newPosition;
                }
            }
            else if (waypoints.Count > 0)
            {
                aStarMovement.CalculateGridSteps(robbiPosition.Value, waypoints[0]);
            }
        }

        #endregion
    }
}
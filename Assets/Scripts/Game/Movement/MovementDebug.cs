using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Debug")]
    public class MovementDebug : MonoBehaviour
    {
        #region Properties and Fields

        private static MovementDebug instance;

        private MovementManager movementManager;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Debug.Assert(instance == null, "Multiple MovementDebug scripts found");
            instance = this;
            movementManager = GetComponent<MovementManager>();
        }

        #endregion

        #region Debug API

        public static void SetPlaceableWaypoints(int waypoints)
        {
            instance.movementManager.remainingWaypointsPlaceable.value = waypoints;
        }

        public static void SetDebugMovement(bool debugMovement)
        {
            instance.movementManager.DebugMovement = debugMovement;
        }

        #endregion
    }
}

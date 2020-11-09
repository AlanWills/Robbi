using Robbi.Parameters;
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

        public TilemapValue movementTilemap;
        public IntValue remainingWaypointsPlaceable;

        #endregion

        #region Unity Methods

        private void Start()
        {
            movementTilemap.value.enabled = false;

#if DEVELOPMENT_BUILD || UNITY_EDITOR
            Debug.Assert(instance == null, "Multiple MovementDebug scripts found");
            instance = this;
#else
            GameObject.Destroy(gameObject);
#endif
        }

        #endregion

        #region Debug API

        public static void SetPlaceableWaypoints(int waypoints)
        {
            instance.remainingWaypointsPlaceable.value = waypoints;
        }

        public static void SetDebugMovement(bool debugMovement)
        {
            instance.movementTilemap.value.enabled = debugMovement;
        }

        #endregion
    }
}

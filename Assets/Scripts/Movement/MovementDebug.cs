using Celeste.Parameters;
using Celeste.Viewport;
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
        public UIntValue remainingFuel;

        #endregion

        #region Unity Methods

        private void Start()
        {
            SetDebugMovementImpl(false);

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
            instance.SetPlaceableWaypointsImpl(waypoints);
        }

        private void SetPlaceableWaypointsImpl(int waypoints)
        {
            remainingWaypointsPlaceable.Value = waypoints;
        }

        public static void AddFuel(int fuel)
        {
            instance.AddFuelImpl(fuel);
        }

        private void AddFuelImpl(int fuel)
        {
            remainingFuel.Value = (uint)Math.Max(0, (int)remainingFuel.Value + fuel);
        }

        public static void SetDebugMovement(bool debugMovement)
        {
            instance.SetDebugMovementImpl(debugMovement);
        }

        private void SetDebugMovementImpl(bool debugMovement)
        {
            movementTilemap.Value.GetComponent<TilemapRenderer>().enabled = debugMovement;
        }

        #endregion
    }
}

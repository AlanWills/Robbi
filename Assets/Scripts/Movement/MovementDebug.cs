using Celeste.Parameters;
using Celeste.Tilemaps;
using System;
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

        private void OnEnable()
        {
            SetDebugMovementImpl(false);
        }

        #endregion

        #region Debug API

        public static void AddPlaceableWaypoints(int waypoints)
        {
            instance.AddPlaceableWaypointsImpl(waypoints);
        }

        private void AddPlaceableWaypointsImpl(int waypoints)
        {
            remainingWaypointsPlaceable.Value += waypoints;
        }

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

        public static bool IsDebugMovementEnabled()
        {
            return instance.IsDebugMovementEnabledImpl();
        }

        private bool IsDebugMovementEnabledImpl()
        {
            return movementTilemap.Value.GetComponent<TilemapRenderer>().enabled;
        }

        #endregion
    }
}

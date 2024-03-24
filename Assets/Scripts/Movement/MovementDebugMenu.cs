using Celeste.Debug.Menus;
using UnityEngine;

namespace Robbi.Movement
{
    [CreateAssetMenu(fileName = nameof(MovementDebugMenu), menuName = "Robbi/Movement/Movement Debug Menu")]
    public class MovementDebugMenu : DebugMenu
    {
        protected override void OnDrawMenu()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Placeable Waypoints:");

                DrawPlaceableWaypointsButton(-5);
                DrawPlaceableWaypointsButton(-1);
                DrawPlaceableWaypointsButton(1);
                DrawPlaceableWaypointsButton(5);
            }

            if (Application.isPlaying)
            {
                bool oldDebugMovement = MovementDebug.IsDebugMovementEnabled();
                bool debugMovement = GUILayout.Toggle(oldDebugMovement, "Debug Movement");

                if (oldDebugMovement != debugMovement)
                {
                    MovementDebug.SetDebugMovement(debugMovement);
                }
            }
        }

        private void DrawPlaceableWaypointsButton(int value)
        {
            if (GUILayout.Button(value.ToString()))
            {
                MovementDebug.AddPlaceableWaypoints(value);
            }
        }
    }
}
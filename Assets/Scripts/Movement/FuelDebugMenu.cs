using Celeste.Debug.Menus;
using UnityEngine;

namespace Robbi.Movement
{
    [CreateAssetMenu(fileName = nameof(FuelDebugMenu), menuName = "Robbi/Movement/Fuel Debug Menu")]
    public class FuelDebugMenu : DebugMenu
    {
        protected override void OnDrawMenu()
        {
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Fuel");

                DrawAddFuelButton(-10);
                DrawAddFuelButton(-1);
                DrawAddFuelButton(1);
                DrawAddFuelButton(10);
            }
        }

        private void DrawAddFuelButton(int value)
        {
            if (GUILayout.Button(value.ToString()))
            {
                MovementDebug.AddFuel(value);
            }
        }
    }
}
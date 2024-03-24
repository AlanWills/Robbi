using Celeste.Debug.Menus;
using Celeste.Tools;
using System;
using UnityEngine;

namespace Robbi.Boosters
{
    [CreateAssetMenu(fileName = nameof(BoostersDebugMenu), menuName = "Robbi/Boosters/Boosters Debug Menu")]
    public class BoostersDebugMenu : DebugMenu
    {
        [SerializeField] private BoostersRecord boostersRecord;

        protected override void OnDrawMenu()
        {
            boostersRecord.NumWaypointBoosters = DrawBoosterField("Waypoint", boostersRecord.NumWaypointBoosters);
            boostersRecord.NumInteractBoosters = DrawBoosterField("Interact", boostersRecord.NumInteractBoosters);
            boostersRecord.NumDoorToggleBoosters = DrawBoosterField("Door Toggle", boostersRecord.NumDoorToggleBoosters);
        }

        private uint DrawBoosterField(string name, uint currentBoosters)
        {
            using (new GUILayout.HorizontalScope())
            {
                uint newBoosters = GUIExtensions.UIntField(name, currentBoosters);

                if (GUILayout.Button("None", GUILayout.MinWidth(40)))
                {
                    newBoosters = 0;
                }

                if (GUILayout.Button("-1", GUILayout.MinWidth(40)))
                {
                    --newBoosters;
                }

                if (GUILayout.Button("+1", GUILayout.MinWidth(40)))
                {
                    ++newBoosters;
                }

                return Math.Max(0, newBoosters);
            }
        }
    }
}
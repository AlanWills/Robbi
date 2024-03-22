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
            boostersRecord.NumWaypointBoosters = DrawBoosterField(boostersRecord.NumWaypointBoosters);
            boostersRecord.NumInteractBoosters = DrawBoosterField(boostersRecord.NumInteractBoosters);
            boostersRecord.NumDoorToggleBoosters = DrawBoosterField(boostersRecord.NumDoorToggleBoosters);
        }

        private uint DrawBoosterField(uint currentBoosters)
        {
            using (new GUILayout.HorizontalScope())
            {
                uint newBoosters = GUIExtensions.UIntField(currentBoosters);

                if (GUILayout.Button("None"))
                {
                    newBoosters = 0;
                }

                if (GUILayout.Button("-1"))
                {
                    --newBoosters;
                }

                if (GUILayout.Button("+1"))
                {
                    ++newBoosters;
                }

                return Math.Max(0, newBoosters);
            }
        }
    }
}
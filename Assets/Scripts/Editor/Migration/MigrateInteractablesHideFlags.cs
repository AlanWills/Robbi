using Robbi.DataSystem;
using Robbi.FSM;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateInteractablesHideFlags
    {
        [MenuItem("Robbi/Migration/Migrate Interactables Hide Flags")]
        public static void MenuItem()
        {
            foreach (string interactableGuid in AssetDatabase.FindAssets("t:Interactable"))
            {
                string interactablePath = AssetDatabase.GUIDToAssetPath(interactableGuid);

                foreach (UnityEngine.Object obj in AssetDatabase.LoadAllAssetsAtPath(interactablePath))
                {
                    AssetUtility.ApplyHideFlags(obj, HideFlags.HideInHierarchy);
                }
            }
        }
    }
}

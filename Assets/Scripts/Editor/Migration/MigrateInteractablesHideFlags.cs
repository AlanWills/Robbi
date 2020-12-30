using CelesteEditor.Tools;
using UnityEditor;
using UnityEngine;

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

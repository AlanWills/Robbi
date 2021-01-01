using Celeste.Tilemaps;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateInteractableMarkers
    {
        [MenuItem("Robbi/Migration/Migrate Interactable Markers")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (levelFolder.Index < 10)
                {
                    continue;
                }

                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                Transform interactables = levelPrefab.transform.Find("Interactables");

                for (int i = interactables.childCount - 1; i > 0; i -= 2)
                {
                    Transform newMarker = interactables.GetChild(i);
                    Transform oldMarker = interactables.GetChild(i - 1);
                    newMarker.localPosition = oldMarker.localPosition;

                    GameObject.DestroyImmediate(oldMarker.gameObject, true);
                    EditorUtility.SetDirty(newMarker);
                }

                EditorUtility.SetDirty(levelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

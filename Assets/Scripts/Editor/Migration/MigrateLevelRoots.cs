using Celeste.Events;
using Celeste.Tools;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Migration
{
    public static class MigrateLevelRoots
    {
        [MenuItem("Robbi/Migration/Migrate Level Roots")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.centreCamera = EditorOnly.FindAsset<Vector3Event>("CentreCamera");

                EditorUtility.SetDirty(levelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

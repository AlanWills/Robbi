using Celeste.Tilemaps;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateLevelRoots
    {
        [MenuItem("Robbi/Migration/Migrate Level Roots")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (levelFolder.Index > 40)
                {
                    continue;
                }

                GameObject portalsGameObject = new GameObject("Portals", typeof(Tilemap), typeof(TilemapRenderer));
                portalsGameObject.GetComponent<TilemapRenderer>().sortingLayerName = "Portals";
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                GameObject levelPrefabInstance = PrefabUtility.InstantiatePrefab(levelPrefab) as GameObject;
                portalsGameObject.transform.parent = levelPrefabInstance.transform;

                PrefabUtility.ApplyAddedGameObject(portalsGameObject, levelFolder.PrefabPath, InteractionMode.AutomatedAction);
                UnityEngine.Object.DestroyImmediate(levelPrefabInstance);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (levelFolder.Index > 40)
                {
                    continue;
                }

                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.portalsTilemap = levelPrefab.transform.Find("Portals").GetComponent<Tilemap>();
                levelPrefab.transform.Find("Portals").SetSiblingIndex(2);
                levelRoot.portalsTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>("Assets/Parameters/Level/Tilemaps/Portals.asset");

                EditorUtility.SetDirty(levelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

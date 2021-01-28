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
            //foreach (LevelFolder levelFolder in new LevelFolders())
            //{
            //    GameObject lasersGameObject = new GameObject("Lasers", typeof(Tilemap), typeof(TilemapRenderer));
            //    lasersGameObject.GetComponent<TilemapRenderer>().sortingLayerName = "Lasers";
            //    GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
            //    GameObject levelPrefabInstance = PrefabUtility.InstantiatePrefab(levelPrefab) as GameObject;
            //    lasersGameObject.transform.parent = levelPrefabInstance.transform;

            //    PrefabUtility.ApplyAddedGameObject(lasersGameObject, levelFolder.PrefabPath, InteractionMode.AutomatedAction);
            //    UnityEngine.Object.DestroyImmediate(levelPrefabInstance);
            //}

            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();

            //foreach (LevelFolder levelFolder in new LevelFolders())
            //{
            //    GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
            //    LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
            //    levelRoot.lasersTilemap = levelPrefab.transform.Find("Lasers").GetComponent<Tilemap>();
            //    levelPrefab.transform.Find("Lasers").SetSiblingIndex(5);
            //    levelRoot.lasersTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>("Assets/Parameters/Level/Tilemaps/Lasers.asset");

            //    EditorUtility.SetDirty(levelPrefab);
            //}

            //AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }
    }
}

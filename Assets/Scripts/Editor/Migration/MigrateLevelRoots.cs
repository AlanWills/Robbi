using Robbi.DataSystem;
using Robbi.Environment;
using Robbi.FSM;
using Robbi.Levels;
using Robbi.Levels.Elements;
using Robbi.Parameters;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
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
    public static class MigrateLevelRoots
    {
        [MenuItem("Robbi/Migration/Migrate Level Roots")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject collectablesGameObject = new GameObject("Collectables", typeof(Tilemap), typeof(TilemapRenderer));
                collectablesGameObject.GetComponent<TilemapRenderer>().sortingLayerName = "Collectables";
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                GameObject levelPrefabInstance = PrefabUtility.InstantiatePrefab(levelPrefab) as GameObject;
                collectablesGameObject.transform.parent = levelPrefabInstance.transform;

                PrefabUtility.ApplyAddedGameObject(collectablesGameObject, levelFolder.PrefabPath, InteractionMode.AutomatedAction);
                UnityEngine.Object.DestroyImmediate(levelPrefabInstance);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.collectablesTilemap = levelPrefab.transform.Find("Collectables").GetComponent<Tilemap>();
                levelPrefab.transform.Find("Collectables").SetSiblingIndex(5);
                levelRoot.collectablesTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>("Assets/Parameters/Level/Tilemaps/Collectables.asset");

                EditorUtility.SetDirty(levelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

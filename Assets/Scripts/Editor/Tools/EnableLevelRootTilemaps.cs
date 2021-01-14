using CelesteEditor.Tools;
using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using static RobbiEditor.LevelDirectories;

namespace RobbiEditor.Tools
{
    public static class EnableLevelRootTilemaps
    {
        [MenuItem("Robbi/Tools/Enable Level Root Tilemaps")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = gameObject.GetComponent<LevelRoot>();
                levelRoot.corridorsTilemap.gameObject.SetActive(true);
                levelRoot.destructibleCorridorsTilemap.gameObject.SetActive(true);
                levelRoot.portalsTilemap.gameObject.SetActive(true);
                levelRoot.exitsTilemap.gameObject.SetActive(true);
                levelRoot.doorsTilemap.gameObject.SetActive(true);
                levelRoot.interactablesTilemap.gameObject.SetActive(true);
                levelRoot.collectablesTilemap.gameObject.SetActive(true);
                levelRoot.movementTilemap.gameObject.SetActive(true);

                EditorUtility.SetDirty(levelRoot);
                EditorUtility.SetDirty(gameObject);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

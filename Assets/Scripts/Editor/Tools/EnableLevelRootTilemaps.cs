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
using UnityEngine.Tilemaps;
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
                CheckTilemap(levelRoot.corridorsTilemap);
                CheckTilemap(levelRoot.destructibleCorridorsTilemap);
                CheckTilemap(levelRoot.portalsTilemap);
                CheckTilemap(levelRoot.exitsTilemap);
                CheckTilemap(levelRoot.doorsTilemap);
                CheckTilemap(levelRoot.interactablesTilemap);
                CheckTilemap(levelRoot.collectablesTilemap);
                CheckTilemap(levelRoot.movementTilemap);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void CheckTilemap(Tilemap tilemap)
        {
            if (!tilemap.gameObject.activeSelf)
            {
                tilemap.gameObject.SetActive(true);
                EditorUtility.SetDirty(tilemap.gameObject);
            }
        }
    }
}

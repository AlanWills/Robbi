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
    public static class ConfigureLevelRootTilemaps
    {
        [MenuItem("Robbi/Tools/Configure Level Root Tilemaps")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject gameObject = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = gameObject.GetComponent<LevelRoot>();
                EnableTilemap(levelRoot.corridorsTilemap);
                EnableTilemap(levelRoot.destructibleCorridorsTilemap);
                EnableTilemap(levelRoot.portalsTilemap);
                EnableTilemap(levelRoot.exitsTilemap);
                EnableTilemap(levelRoot.doorsTilemap);
                EnableTilemap(levelRoot.interactablesTilemap);
                EnableTilemap(levelRoot.collectablesTilemap);

                if (levelRoot.movementTilemap.GetComponent<TilemapRenderer>().enabled)
                {
                    levelRoot.movementTilemap.GetComponent<TilemapRenderer>().enabled = false;
                    EditorUtility.SetDirty(levelRoot.movementTilemap.gameObject);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void EnableTilemap(Tilemap tilemap)
        {
            if (!tilemap.gameObject.activeSelf)
            {
                tilemap.gameObject.SetActive(true);
                tilemap.GetComponent<TilemapRenderer>().enabled = true;
                EditorUtility.SetDirty(tilemap.gameObject);
            }
        }
    }
}

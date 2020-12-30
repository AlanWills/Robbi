using CelesteEditor;
using CelesteEditor.Tools;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Tools
{
    public class ReplaceTile : ScriptableWizard
    {
        #region Properties and Fields

        public uint startLevelIndex = 0;
        public uint endLevelIndex = 0;
        public List<TileBase> originalTiles;
        public List<TileBase> replacementTiles;

        #endregion

        #region GUI

        private void OnEnable()
        {
            endLevelIndex = LevelManager.EditorOnly_Load().LatestAvailableLevel_DefaultValue;
        }

        private void OnWizardCreate()
        {
            Close();
        }

        // Use other button so we can keep the wizard open after doing something
        private void OnWizardOtherButton()
        {
            LogUtility.Clear();

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (startLevelIndex <= levelFolder.Index && levelFolder.Index <= endLevelIndex)
                {
                    GameObject level = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                    LevelRoot levelRoot = level.GetComponent<LevelRoot>();

                    ReplaceTilesIn(levelRoot.collectablesTilemap);
                    ReplaceTilesIn(levelRoot.corridorsTilemap);
                    ReplaceTilesIn(levelRoot.destructibleCorridorsTilemap);
                    ReplaceTilesIn(levelRoot.doorsTilemap);
                    ReplaceTilesIn(levelRoot.exitsTilemap);
                    ReplaceTilesIn(levelRoot.interactablesTilemap);
                    ReplaceTilesIn(levelRoot.movementTilemap);
                }
                else
                {
                    Debug.LogFormat("Skipping level {0} as it is not in specified bounds", levelFolder.Index);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        #endregion

        #region Tile Replacement

        private void ReplaceTilesIn(Tilemap tilemap)
        {
            for (int i = 0; i < originalTiles.Count; ++i)
            {
                TileBase originalTile = originalTiles[i];
                if (originalTile == null)
                {
                    continue;
                }

                if (!tilemap.ContainsTile(originalTile))
                {
                    Debug.LogFormat("Tile {0} not found in tilemap {1}.  Skipping...", originalTile.name, tilemap.name);
                    continue;
                }

                BoundsInt tilemapBounds = tilemap.cellBounds;

                for (int y = 0; y < tilemapBounds.size.y; ++y)
                {
                    for (int x = 0; x < tilemapBounds.size.x; ++x)
                    {
                        Vector3Int position = tilemapBounds.min;
                        position.x += x;
                        position.y += y;

                        if (tilemap.HasTile(position) && tilemap.GetTile(position) == originalTile)
                        {
                            tilemap.SetTile(position, replacementTiles[i]);
                            EditorUtility.SetDirty(tilemap);
                        }
                    }
                }
            }

            tilemap.RefreshAllTiles();
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Tools/Replace Tile")]
        public static void ShowReplaceTileWizard()
        {
            ScriptableWizard.DisplayWizard<ReplaceTile>("Replace Tile", "Close", "Replace");
        }

        #endregion
    }
}
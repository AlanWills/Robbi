using CelesteEditor;
using CelesteEditor.Tools;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
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
        public TileBase originalTile;
        public TileBase replacementTile;

        #endregion

        #region GUI

        private void OnEnable()
        {
            endLevelIndex = LevelManager.EditorOnly_Load().LatestAvailableLevel_DefaultValue;
        }

        private void OnWizardCreate()
        {
            LogUtility.Clear();

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (startLevelIndex <= levelFolder.Index && levelFolder.Index <= endLevelIndex)
                {
                    GameObject level = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                    LevelRoot levelRoot = level.GetComponent<LevelRoot>();

                    ReplaceTileIn(levelRoot.collectablesTilemap);
                    ReplaceTileIn(levelRoot.corridorsTilemap);
                    ReplaceTileIn(levelRoot.destructibleCorridorsTilemap);
                    ReplaceTileIn(levelRoot.doorsTilemap);
                    ReplaceTileIn(levelRoot.exitsTilemap);
                    ReplaceTileIn(levelRoot.interactablesTilemap);
                    ReplaceTileIn(levelRoot.movementTilemap);
                }
                else
                {
                    Debug.LogFormat("Skipping level {0} as it is not in specified bounds", levelFolder.Index);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void OnWizardOtherButton()
        {
            Close();
        }

        #endregion

        #region Tile Replacement

        private void ReplaceTileIn(Tilemap tilemap)
        {
            if (!tilemap.ContainsTile(originalTile))
            {
                Debug.LogFormat("Tile {0} not found in tilemap {1}.  Skipping...", originalTile.name, tilemap.name);
                return;
            }

            BoundsInt tilemapBounds = tilemap.cellBounds;

            for (int y = 0; y < tilemapBounds.size.y; ++y)
            {
                for (int x = 0; x < tilemapBounds.size.x; ++x)
                {
                    Vector3Int position = tilemapBounds.min;
                    position.x += x;
                    position.y += tilemapBounds.size.x * y;

                    if (tilemap.HasTile(position) && tilemap.GetTile(position) == originalTile)
                    {
                        tilemap.SetTile(position, replacementTile);
                        EditorUtility.SetDirty(tilemap);
                    }
                }
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Tools/Replace Tile")]
        public static void ShowReplaceTileWizard()
        {
            ScriptableWizard.DisplayWizard<ReplaceTile>("Replace Tile", "Replace", "Close");
        }

        #endregion
    }
}
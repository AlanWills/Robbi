using Celeste.Viewport;
using Robbi.Levels;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateTilemaps
    {
        [MenuItem("Robbi/Migration/Migrate Tilemaps")]
        public static void MenuItem()
        {
            Material material = AssetDatabase.LoadAssetAtPath<Material>("Assets/Materials/Tile.mat");
            if (material == null)
            {
                Debug.LogAssertion("Could not find tile material");
                return;
            }

            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                LevelRoot levelRoot = levelPrefab.GetComponent<LevelRoot>();
                levelRoot.collectablesTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.corridorsTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.destructibleCorridorsTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.doorsTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.exitsTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.interactablesTilemap.GetComponent<TilemapRenderer>().material = material;
                levelRoot.movementTilemap.GetComponent<TilemapRenderer>().material = material;

                EditorUtility.SetDirty(levelRoot);
                EditorUtility.SetDirty(levelPrefab);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}

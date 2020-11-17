using Robbi.Levels;
using Robbi.Parameters;
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
    public static class AddLevelRootComponent
    {
        [MenuItem("Robbi/Migration/Migrate Level Root")]
        public static void MigrateHorizontalDoors()
        {
            TilemapValue corridorsTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.CORRIDORS_TILEMAP);
            if (corridorsTilemapValue == null)
            {
                Debug.LogAssertion("Corridors tilemap could not be found");
                return;
            }
            
            TilemapValue destructibleCorridorsTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.DESTRUCTIBLE_CORRIDORS_TILEMAP);
            if (destructibleCorridorsTilemapValue == null)
            {
                Debug.LogAssertion("Destructible Corridors tilemap could not be found");
                return;
            }

            TilemapValue doorsTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.DOORS_TILEMAP);
            if (doorsTilemapValue == null)
            {
                Debug.LogAssertion("Doors tilemap could not be found");
                return;
            }

            TilemapValue exitsTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.EXIT_TILEMAP);
            if (exitsTilemapValue == null)
            {
                Debug.LogAssertion("Exits tilemap could not be found");
                return;
            }

            TilemapValue interactablesTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.INTERACTABLES_TILEMAP);
            if (interactablesTilemapValue == null)
            {
                Debug.LogAssertion("Interactables tilemap could not be found");
                return;
            }

            TilemapValue movementTilemapValue = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.MOVEMENT_TILEMAP);
            if (movementTilemapValue == null)
            {
                Debug.LogAssertion("Movement tilemap could not be found");
                return;
            }

            int i = 0;
            string levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
            GameObject levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);

            while (levelPrefab != null)
            {
                LevelRoot levelRoot = levelPrefab.AddComponent<LevelRoot>();
                levelRoot.corridorsTilemapValue = corridorsTilemapValue;
                levelRoot.destructibleCorridorsTilemapValue = destructibleCorridorsTilemapValue;
                levelRoot.doorsTilemapValue = doorsTilemapValue;
                levelRoot.exitsTilemapValue = exitsTilemapValue;
                levelRoot.interactablesTilemapValue = interactablesTilemapValue;
                levelRoot.movementTilemapValue = movementTilemapValue;

                levelRoot.corridorsTilemap = levelRoot.transform.Find("Corridors").GetComponent<Tilemap>();
                levelRoot.destructibleCorridorsTilemap = levelRoot.transform.Find("DestructibleCorridors").GetComponent<Tilemap>();
                levelRoot.doorsTilemap = levelRoot.transform.Find("Doors").GetComponent<Tilemap>();
                levelRoot.exitsTilemap = levelRoot.transform.Find("Exits").GetComponent<Tilemap>();
                levelRoot.interactablesTilemap = levelRoot.transform.Find("Interactables").GetComponent<Tilemap>();
                levelRoot.movementTilemap = levelRoot.transform.Find("Movement").GetComponent<Tilemap>();

                EditorUtility.SetDirty(levelRoot);

                ++i;
                levelFolderPath = string.Format("Assets/Levels/Level{0}/Level{0}.prefab", i);
                levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolderPath);
            }

            AssetDatabase.SaveAssets();
        }
    }
}

using Robbi.Levels;
using Robbi.Levels.Elements;
using RobbiEditor.Iterators;
using RobbiEditor.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Migration
{
    public static class MigrateDoorStates
    {
        [MenuItem("Robbi/Migration/Migrate Door States")]
        public static void MenuItem()
        {
            foreach (LevelFolder levelFolder in new LevelFolders())
            {
                if (levelFolder.Index < 15)
                {
                    continue;
                }

                GameObject levelGameObject = AssetDatabase.LoadAssetAtPath<GameObject>(levelFolder.PrefabPath);
                Tilemap doorsTilemap = levelGameObject.GetComponent<LevelRoot>().doorsTilemap;

                Level level = AssetDatabase.LoadAssetAtPath<Level>(levelFolder.LevelDataPath);
                foreach (Door door in level.doors)
                {
                    door.startingState = doorsTilemap.HasTile(door.position) ? DoorState.Closed : DoorState.Opened;
                    EditorUtility.SetDirty(door);
                }
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
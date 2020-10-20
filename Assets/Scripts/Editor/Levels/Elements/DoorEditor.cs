using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        #region Menu Items

        [MenuItem("Assets/Create/Robbi/Levels/Horizontal Door")]
        public static void CreateHorizontalDoor()
        {
            CreateHorizontalDoor("Door");
        }

        [MenuItem("Assets/Create/Robbi/Levels/Vertical Door")]
        public static void CreateVerticalDoor()
        {
            CreateVerticalDoor("Door");
        }

        #endregion

        #region Utility

        public static void CreateHorizontalDoor(string name)
        {
            CreateDoor(name, Direction.Horizontal, TileFiles.HORIZONTAL_LEFT_OPEN_DOOR_TILE, TileFiles.HORIZONTAL_RIGHT_OPEN_DOOR_TILE);
        }

        public static void CreateVerticalDoor(string name)
        {
            CreateDoor(name, Direction.Vertical, TileFiles.VERTICAL_LEFT_OPEN_DOOR_TILE, TileFiles.VERTICAL_RIGHT_OPEN_DOOR_TILE);
        }

        private static void CreateDoor(string name, Direction direction, string leftTile, string rightTile)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.name = name;
            door.direction = direction;
            door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(leftTile);
            door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(rightTile);

            Debug.Assert(door.leftOpenTile != null, "Default left open tile could not be found for door.");
            Debug.Assert(door.rightOpenTile != null, "Default right open tile could not be found for door.");

            AssetUtility.CreateAsset(door);
            AddressablesUtility.SetAddressableGroup(door, AddressablesConstants.LEVELS_GROUP);
        }

        #endregion
    }
}

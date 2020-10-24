using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
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

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Green")]
        public static void CreateHorizontalGreenDoor()
        {
            CreateHorizontalDoor("Door", GetSelectionObjectPath(), TileFiles.HORIZONTAL_GREEN_CLOSED_DOOR_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Red")]
        public static void CreateHorizontalRedDoor()
        {
            CreateHorizontalDoor("Door", GetSelectionObjectPath(), TileFiles.HORIZONTAL_RED_CLOSED_DOOR_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Blue")]
        public static void CreateHorizontalBlueDoor()
        {
            CreateHorizontalDoor("Door", GetSelectionObjectPath(), TileFiles.HORIZONTAL_BLUE_CLOSED_DOOR_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Green")]
        public static void CreateVerticalGreenDoor()
        {
            CreateVerticalDoor("Door", GetSelectionObjectPath(), TileFiles.VERTICAL_GREEN_CLOSED_DOOR_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Red")]
        public static void CreateVerticalRedDoor()
        {
            CreateVerticalDoor("Door", GetSelectionObjectPath(), TileFiles.VERTICAL_RED_CLOSED_DOOR_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Blue")]
        public static void CreateVerticalBlueDoor()
        {
            CreateVerticalDoor("Door", GetSelectionObjectPath(), TileFiles.VERTICAL_BLUE_CLOSED_DOOR_TILE);
        }

        #endregion

        #region Utility

        public static void CreateHorizontalDoor(string name, string path, string closedTile)
        {
            CreateDoor(name, path, Direction.Horizontal, closedTile, TileFiles.HORIZONTAL_LEFT_OPEN_DOOR_TILE, TileFiles.HORIZONTAL_RIGHT_OPEN_DOOR_TILE);
        }

        public static void CreateVerticalDoor(string name, string path, string closedTile)
        {
            CreateDoor(name, path, Direction.Vertical, closedTile, TileFiles.VERTICAL_LEFT_OPEN_DOOR_TILE, TileFiles.VERTICAL_RIGHT_OPEN_DOOR_TILE);
        }

        private static void CreateDoor(string name, string path, Direction direction, string closedTile, string leftTile, string rightTile)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.name = name;
            door.direction = direction;
            door.closedTile = AssetDatabase.LoadAssetAtPath<Tile>(closedTile);
            door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(leftTile);
            door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(rightTile);

            Debug.Assert(door.closedTile != null, "Closed tile could not be found for door.");
            Debug.Assert(door.leftOpenTile != null, "Default left open tile could not be found for door.");
            Debug.Assert(door.rightOpenTile != null, "Default right open tile could not be found for door.");

            AssetUtility.CreateAsset(door, path);
            AddressablesUtility.SetAddressableGroup(door, AddressablesConstants.LEVELS_GROUP);
        }

        private static string GetSelectionObjectPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }

        #endregion
    }
}

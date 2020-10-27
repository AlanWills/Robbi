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
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal,
                TileFiles.HORIZONTAL_GREEN_CLOSED_DOOR_TILE,
                TileFiles.HORIZONTAL_GREEN_OPEN_DOOR_LEFT_TILE,
                TileFiles.HORIZONTAL_GREEN_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Red")]
        public static void CreateHorizontalRedDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal,
                TileFiles.HORIZONTAL_RED_CLOSED_DOOR_TILE,
                TileFiles.HORIZONTAL_RED_OPEN_DOOR_LEFT_TILE,
                TileFiles.HORIZONTAL_RED_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Blue")]
        public static void CreateHorizontalBlueDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal,
                TileFiles.HORIZONTAL_BLUE_CLOSED_DOOR_TILE,
                TileFiles.HORIZONTAL_BLUE_OPEN_DOOR_LEFT_TILE,
                TileFiles.HORIZONTAL_BLUE_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Grey")]
        public static void CreateHorizontalGreyDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal,
                TileFiles.HORIZONTAL_GREY_CLOSED_DOOR_TILE,
                TileFiles.HORIZONTAL_GREY_OPEN_DOOR_LEFT_TILE,
                TileFiles.HORIZONTAL_GREY_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Green")]
        public static void CreateVerticalGreenDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical,
                TileFiles.VERTICAL_GREEN_CLOSED_DOOR_TILE,
                TileFiles.VERTICAL_GREEN_OPEN_DOOR_LEFT_TILE,
                TileFiles.VERTICAL_GREEN_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Red")]
        public static void CreateVerticalRedDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical,
                TileFiles.VERTICAL_RED_CLOSED_DOOR_TILE,
                TileFiles.VERTICAL_RED_OPEN_DOOR_LEFT_TILE,
                TileFiles.VERTICAL_RED_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Blue")]
        public static void CreateVerticalBlueDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical,
                TileFiles.VERTICAL_BLUE_CLOSED_DOOR_TILE,
                TileFiles.VERTICAL_BLUE_OPEN_DOOR_LEFT_TILE,
                TileFiles.VERTICAL_BLUE_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Grey")]
        public static void CreateVerticalGreyDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical,
                TileFiles.VERTICAL_GREY_CLOSED_DOOR_TILE,
                TileFiles.VERTICAL_GREY_OPEN_DOOR_LEFT_TILE,
                TileFiles.VERTICAL_GREY_OPEN_DOOR_LEFT_TILE);
        }

        [MenuItem("Robbi/Migrate Doors")]
        public static void MigrateHorizontalDoors()
        {
            foreach (string doorGuid in AssetDatabase.FindAssets("t:Door"))
            {
                string doorPath = AssetDatabase.GUIDToAssetPath(doorGuid);
                Door door = AssetDatabase.LoadAssetAtPath<Door>(doorPath);

                if (door.direction == Direction.Horizontal)
                {
                    string closedTilePath = AssetDatabase.GetAssetPath(door.closedTile);

                    if (closedTilePath == TileFiles.HORIZONTAL_GREEN_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_GREEN_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_GREEN_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find green horizontal door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find green horizontal door right tile");
                    }
                    else if (closedTilePath == TileFiles.HORIZONTAL_RED_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_RED_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_RED_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find red horizontal door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find red horizontal door right tile");
                    }
                    else if (closedTilePath == TileFiles.HORIZONTAL_BLUE_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_BLUE_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_BLUE_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find blue horizontal door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find blue horizontal door right tile");
                    }
                    else if (closedTilePath == TileFiles.HORIZONTAL_GREY_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_GREY_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.HORIZONTAL_GREY_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find grey horizontal door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find grey horizontal door right tile");
                    }
                    else
                    {
                        Debug.LogAssertionFormat("Could not find closed tile for {0}", doorPath);
                    }
                }
                else if (door.direction == Direction.Vertical)
                {
                    string assetPath = AssetDatabase.GetAssetPath(door.closedTile);

                    if (assetPath == TileFiles.VERTICAL_GREEN_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_GREEN_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_GREEN_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find green vertical door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find green vertical door right tile");
                    }
                    else if (assetPath == TileFiles.VERTICAL_RED_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_RED_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_RED_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find red vertical door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find red vertical door right tile");
                    }
                    else if (assetPath == TileFiles.VERTICAL_BLUE_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_BLUE_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_BLUE_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find blue vertical door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find blue vertical door right tile");
                    }
                    else if (assetPath == TileFiles.VERTICAL_GREY_CLOSED_DOOR_TILE)
                    {
                        door.leftOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_GREY_OPEN_DOOR_LEFT_TILE);
                        door.rightOpenTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.VERTICAL_GREY_OPEN_DOOR_RIGHT_TILE);
                        Debug.Assert(door.leftOpenTile, "Could not find grey vertical door left tile");
                        Debug.Assert(door.rightOpenTile, "Could not find grey vertical door right tile");
                    }
                    else
                    {
                        Debug.LogAssertionFormat("Could not find closed tile for {0}", assetPath);
                    }
                }
            }
        }

        #endregion

        #region Utility

        public static void CreateDoor(string name, string path, Direction direction, string closedTile, string leftTile, string rightTile)
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

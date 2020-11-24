using Robbi.Levels.Elements;
using RobbiEditor.Constants;
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
    public static class MigrateHideFlags
    {
        [MenuItem("Robbi/Migration/Migrate Doors")]
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
    }
}

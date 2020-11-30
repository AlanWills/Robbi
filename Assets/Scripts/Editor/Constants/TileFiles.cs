using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Constants
{
    public static class TileFiles
    {
        public const string HORIZONTAL_GREEN_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreenClosedDoor.asset";
        public const string HORIZONTAL_GREEN_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreenOpenDoorLeft.asset";
        public const string HORIZONTAL_GREEN_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreenOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> HORIZONTAL_GREEN_DOOR = new Tuple<string, string, string>(
            HORIZONTAL_GREEN_CLOSED_DOOR_TILE, HORIZONTAL_GREEN_OPEN_DOOR_LEFT_TILE, HORIZONTAL_GREEN_OPEN_DOOR_RIGHT_TILE);

        public const string HORIZONTAL_RED_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalRedClosedDoor.asset";
        public const string HORIZONTAL_RED_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalRedOpenDoorLeft.asset";
        public const string HORIZONTAL_RED_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalRedOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> HORIZONTAL_RED_DOOR = new Tuple<string, string, string>(
            HORIZONTAL_RED_CLOSED_DOOR_TILE, HORIZONTAL_RED_OPEN_DOOR_LEFT_TILE, HORIZONTAL_RED_OPEN_DOOR_RIGHT_TILE);

        public const string HORIZONTAL_BLUE_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalBlueClosedDoor.asset";
        public const string HORIZONTAL_BLUE_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalBlueOpenDoorLeft.asset";
        public const string HORIZONTAL_BLUE_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalBlueOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> HORIZONTAL_BLUE_DOOR = new Tuple<string, string, string>(
            HORIZONTAL_BLUE_CLOSED_DOOR_TILE, HORIZONTAL_BLUE_OPEN_DOOR_LEFT_TILE, HORIZONTAL_BLUE_OPEN_DOOR_RIGHT_TILE);

        public const string HORIZONTAL_GREY_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreyClosedDoor.asset";
        public const string HORIZONTAL_GREY_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreyOpenDoorLeft.asset";
        public const string HORIZONTAL_GREY_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "HorizontalGreyOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> HORIZONTAL_GREY_DOOR = new Tuple<string, string, string>(
           HORIZONTAL_GREY_CLOSED_DOOR_TILE, HORIZONTAL_GREY_OPEN_DOOR_LEFT_TILE, HORIZONTAL_GREY_OPEN_DOOR_RIGHT_TILE);

        public const string VERTICAL_GREEN_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreenClosedDoor.asset";
        public const string VERTICAL_GREEN_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreenOpenDoorLeft.asset";
        public const string VERTICAL_GREEN_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreenOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> VERTICAL_GREEN_DOOR = new Tuple<string, string, string>(
            VERTICAL_GREEN_CLOSED_DOOR_TILE, VERTICAL_GREEN_OPEN_DOOR_LEFT_TILE, VERTICAL_GREEN_OPEN_DOOR_RIGHT_TILE);

        public const string VERTICAL_RED_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalRedClosedDoor.asset";
        public const string VERTICAL_RED_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalRedOpenDoorLeft.asset";
        public const string VERTICAL_RED_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalRedOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> VERTICAL_RED_DOOR = new Tuple<string, string, string>(
            VERTICAL_RED_CLOSED_DOOR_TILE, VERTICAL_RED_OPEN_DOOR_LEFT_TILE, VERTICAL_RED_OPEN_DOOR_RIGHT_TILE);

        public const string VERTICAL_BLUE_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalBlueClosedDoor.asset";
        public const string VERTICAL_BLUE_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalBlueOpenDoorLeft.asset";
        public const string VERTICAL_BLUE_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalBlueOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> VERTICAL_BLUE_DOOR = new Tuple<string, string, string>(
            VERTICAL_BLUE_CLOSED_DOOR_TILE, VERTICAL_BLUE_OPEN_DOOR_LEFT_TILE, VERTICAL_BLUE_OPEN_DOOR_RIGHT_TILE);

        public const string VERTICAL_GREY_CLOSED_DOOR_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreyClosedDoor.asset";
        public const string VERTICAL_GREY_OPEN_DOOR_LEFT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreyOpenDoorLeft.asset";
        public const string VERTICAL_GREY_OPEN_DOOR_RIGHT_TILE = TileDirectories.DOOR_TILES_FULL_PATH + "VerticalGreyOpenDoorRight.asset";
        public static readonly Tuple<string, string, string> VERTICAL_GREY_DOOR = new Tuple<string, string, string>(
            VERTICAL_GREY_CLOSED_DOOR_TILE, VERTICAL_GREY_OPEN_DOOR_LEFT_TILE, VERTICAL_GREY_OPEN_DOOR_RIGHT_TILE);

        public const string TOGGLE_LEFT_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "ToggleLeft.asset";
        public const string TOGGLE_RIGHT_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "ToggleRight.asset";
    }
}

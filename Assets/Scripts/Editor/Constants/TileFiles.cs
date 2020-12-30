using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Constants
{
    public static class TileFiles
    {
        public const string GREEN_TOGGLE_UP_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Green/GreenToggleUp.asset";
        public const string GREEN_TOGGLE_DOWN_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Green/GreenToggleDown.asset";

        public const string RED_TOGGLE_UP_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Red/RedToggleUp.asset";
        public const string RED_TOGGLE_DOWN_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Red/RedToggleDown.asset";

        public const string BLUE_TOGGLE_UP_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Blue/BlueToggleUp.asset";
        public const string BLUE_TOGGLE_DOWN_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Blue/BlueToggleDown.asset";

        public const string BROWN_TOGGLE_UP_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Brown/BrownToggleUp.asset";
        public const string BROWN_TOGGLE_DOWN_TILE = TileDirectories.INTERACTABLES_TILES_FULL_PATH + "Brown/BrownToggleDown.asset";

        public static readonly Tuple<string, string>[] TOGGLE_TILES = new Tuple<string, string>[4]
        {
            new Tuple<string, string>(GREEN_TOGGLE_UP_TILE, GREEN_TOGGLE_DOWN_TILE),
            new Tuple<string, string>(RED_TOGGLE_UP_TILE, RED_TOGGLE_DOWN_TILE),
            new Tuple<string, string>(BLUE_TOGGLE_UP_TILE, BLUE_TOGGLE_DOWN_TILE),
            new Tuple<string, string>(BROWN_TOGGLE_UP_TILE, BROWN_TOGGLE_DOWN_TILE)
        };
    }
}

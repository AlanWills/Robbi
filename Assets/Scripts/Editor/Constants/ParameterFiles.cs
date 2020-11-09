using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static RobbiEditor.ParameterDirectories;

namespace RobbiEditor
{
    public static class ParameterFiles
    {
        public const string PLAYER_LOCAL_POSITION_DATA = PARAMETERS_FULL_PATH + LEVEL_NAME + "PlayerLocalPosition.asset";
        public const string REMAINING_WAYPOINTS_PLACEABLE_DATA = PARAMETERS_FULL_PATH + LEVEL_NAME + "WaypointsRemaining.asset";
        public const string CORRIDORS_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "Corridors.asset";
        public const string DESTRUCTIBLE_CORRIDORS_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "DestructibleCorridors.asset";
        public const string DOORS_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "Doors.asset";
        public const string EXIT_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "Exit.asset";
        public const string INTERACTABLES_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "Interactables.asset";
        public const string MOVEMENT_TILEMAP = PARAMETERS_FULL_PATH + LEVEL_NAME + TILEMAPS_NAME + "Movement.asset";
    }
}

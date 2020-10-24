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
        public const string PLAYER_LOCAL_POSITION_DATA = PARAMETERS_FULL_PATH + "/" + LEVEL_NAME + "/" + "PlayerLocalPosition.asset";
        public const string REMAINING_WAYPOINTS_PLACEABLE_DATA = PARAMETERS_FULL_PATH + "/" + LEVEL_NAME + "/" + "WaypointsRemaining.asset";
        public const string DOORS_TILEMAP_DATA = PARAMETERS_FULL_PATH + "/" + LEVEL_NAME + "/" + TILEMAPS_NAME + "/Doors.asset";
    }
}

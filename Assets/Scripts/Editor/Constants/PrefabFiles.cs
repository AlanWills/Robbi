using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static RobbiEditor.PrefabDirectories;

namespace RobbiEditor
{
    public static class PrefabFiles
    {
        public const string DOOR_OPEN_MARKER_PREFAB = PREFABS_FULL_PATH + "/" + LEVEL_NAME + "/" + "DoorOpenMarker.prefab";
        public const string DOOR_CLOSE_MARKER_PREFAB = PREFABS_FULL_PATH + "/" + LEVEL_NAME + "/" + "DoorCloseMarker.prefab";
        public const string DOOR_TOGGLE_MARKER_PREFAB = PREFABS_FULL_PATH + "/" + LEVEL_NAME + "/" + "DoorToggleMarker.prefab";
    }
}

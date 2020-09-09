using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static RobbiEditor.EventDirectories;

namespace RobbiEditor
{
    public static class EventFiles
    {
        public const string EXIT_REACHED_EVENT = FULL_PATH + "/" + LEVELS_NAME + "/" + "ExitReached.asset";
        public const string DOOR_OPENED_EVENT = FULL_PATH + "/" + LEVELS_NAME + "/" + "DoorOpened.asset";
        public const string MOVED_TO_EVENT = FULL_PATH + "/" + LEVELS_NAME + "/" + "MovedTo.asset";
    }
}

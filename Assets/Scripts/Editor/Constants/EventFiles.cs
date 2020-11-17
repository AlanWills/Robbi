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
        public const string DOOR_OPENED_EVENT = FULL_PATH + LEVEL + DOORS + "DoorOpened.asset";
        public const string DOOR_CLOSED_EVENT = FULL_PATH + LEVEL + DOORS + "DoorClosed.asset";
        public const string DOOR_TOGGLED_EVENT = FULL_PATH + LEVEL + DOORS + "DoorToggled.asset";
        public const string MOVED_TO_EVENT = FULL_PATH + LEVEL + "MovedTo.asset";
        public const string MODIFY_SPEED_EVENT = FULL_PATH + LEVEL + "ModifySpeed.asset";
        public const string RUN_PROGRAM_EVENT = FULL_PATH + LEVEL + "RunProgram.asset";
        public const string LEVEL_WON_EVENT = FULL_PATH + LEVEL + "LevelWon.asset";
        public const string LEVEL_LOSE_WAYPOINT_UNREACHABLE_EVENT = FULL_PATH + LEVEL + "LevelLoseWaypointUnreachable.asset";
        public const string LEVEL_LOSE_OUT_OF_WAYPOINTS_EVENT = FULL_PATH + LEVEL + "LevelLoseOutOfWaypoints.asset";
        public const string INTEGRATION_TEST_PASSED_EVENT = FULL_PATH + TESTING + "IntegrationTestPassed.asset";
        public const string INTEGRATION_TEST_FAILED_EVENT = FULL_PATH + TESTING + "IntegrationTestFailed.asset";
        public const string GAME_OBJECT_LEFT_CLICK_EVENT = FULL_PATH + INPUT + COMMON + "GameObjectLeftClick.asset";
    }
}

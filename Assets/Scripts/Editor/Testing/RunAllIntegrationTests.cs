using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public static class RunAllIntegrationTests
    {
        [MenuItem("Robbi/Testing/Run All Integration Tests")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTests(
                typeof(MainMenuToLevel),
                typeof(MainMenuToLockedLevel),
                typeof(MainMenuToMaxLevel),
                typeof(LevelHomeButton),
                typeof(LevelLoseOutOfWaypoints),
                typeof(LevelLoseOutOfWaypointsMainMenuButton),
                typeof(LevelLoseWaypointUnreachable),
                typeof(LevelLoseWaypointUnreachableMainMenuButton),
                typeof(LevelOptionsButton),
                typeof(LevelRestartButton),
                typeof(LevelWinMainMenuButton),
                typeof(LevelWinRetryButton),
                typeof(PlayLevels));
        }
    }
}

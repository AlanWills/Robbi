using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseWaypointUnreachableDoesNotIncreaseCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Waypoint Unreachable/Level Lose Waypoint Unreachable Does Not Increase Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseWaypointUnreachableDoesNotIncreaseCurrentLevel>();
        }
    }
}

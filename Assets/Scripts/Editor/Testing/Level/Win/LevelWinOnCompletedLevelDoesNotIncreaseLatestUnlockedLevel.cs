using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinOnCompletedLevelDoesNotIncreaseLatestUnlockedLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Win/Level Win On Completed Level Does Not Increase Latest Unlocked Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinOnCompletedLevelDoesNotIncreaseLatestUnlockedLevel>();
        }
    }
}

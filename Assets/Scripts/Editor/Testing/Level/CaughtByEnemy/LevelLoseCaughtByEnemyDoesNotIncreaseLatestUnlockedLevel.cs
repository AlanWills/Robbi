using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseCaughtByEnemyDoesNotIncreaseLatestUnlockedLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Caught By Enemy/Level Lose Caught By Enemy Does Not Increase Latest Unlocked Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseCaughtByEnemyDoesNotIncreaseLatestUnlockedLevel>();
        }
    }
}

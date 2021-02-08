using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseCaughtByEnemyDoesNotIncreaseCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Caught By Enemy/Level Lose Caught By Enemy Does Not Increase Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseCaughtByEnemyDoesNotIncreaseCurrentLevel>();
        }
    }
}

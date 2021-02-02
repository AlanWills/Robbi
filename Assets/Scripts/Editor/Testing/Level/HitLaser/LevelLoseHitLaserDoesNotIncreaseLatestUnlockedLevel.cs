using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseHitLaserDoesNotIncreaseLatestUnlockedLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Hit Laser/Level Lose Hit Laser Does Not Increase Latest Unlocked Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseHitLaserDoesNotIncreaseLatestUnlockedLevel>();
        }
    }
}

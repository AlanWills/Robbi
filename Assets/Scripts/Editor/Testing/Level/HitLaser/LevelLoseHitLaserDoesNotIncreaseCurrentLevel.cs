using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseHitLaserDoesNotIncreaseCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Hit Laser/Level Lose Hit Laser Does Not Increase Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseHitLaserDoesNotIncreaseCurrentLevel>();
        }
    }
}

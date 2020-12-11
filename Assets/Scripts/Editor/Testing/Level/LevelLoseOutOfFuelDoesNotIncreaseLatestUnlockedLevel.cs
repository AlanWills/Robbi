using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseOutOfFuelDoesNotIncreaseLatestUnlockedLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Lose Out Of Fuel Does Not Increase Latest Unlocked Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfFuelDoesNotIncreaseLatestUnlockedLevel>();
        }
    }
}

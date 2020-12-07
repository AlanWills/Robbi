using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelLoseOutOfFuelDoesNotIncreaseCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Lose Out Of Fuel Does Not Increase Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfFuelDoesNotIncreaseCurrentLevel>();
        }
    }
}

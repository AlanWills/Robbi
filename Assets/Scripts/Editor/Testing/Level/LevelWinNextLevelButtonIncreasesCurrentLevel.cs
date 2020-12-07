using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinNextLevelButtonIncreasesCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Win Next Level Button Increases Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinNextLevelButtonIncreasesCurrentLevel>();
        }
    }
}

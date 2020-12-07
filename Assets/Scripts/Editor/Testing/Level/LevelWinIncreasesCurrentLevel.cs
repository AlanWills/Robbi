using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinIncreasesCurrentLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Win Increases Current Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinIncreasesCurrentLevel>();
        }
    }
}

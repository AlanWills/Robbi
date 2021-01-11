using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinNextLevelButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Win/Level Win Next Level Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinNextLevelButton>();
        }
    }
}

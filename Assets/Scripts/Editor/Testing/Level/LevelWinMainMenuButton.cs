using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinMainMenuButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Win Main Menu Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinMainMenuButton>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelWinRetryButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Win Retry Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelWinRetryButton>();
        }
    }
}

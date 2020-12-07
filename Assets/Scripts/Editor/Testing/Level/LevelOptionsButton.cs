using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelOptionsButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Options Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelOptionsButton>();
        }
    }
}

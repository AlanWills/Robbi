using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelOptionsBackButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Options Back Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelOptionsBackButton>();
        }
    }
}

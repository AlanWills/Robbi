using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class PickLevelToLatestAvailableLevel : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Pick Level/Pick Level To Latest Available Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<PickLevelToLatestAvailableLevel>();
        }
    }
}

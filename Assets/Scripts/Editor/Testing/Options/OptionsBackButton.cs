using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsBackButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Back Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsBackButton>();
        }
    }
}

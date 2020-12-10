using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsEnableBatterySaverToggle : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Enable Battery Saver Toggle")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsEnableBatterySaverToggle>();
        }
    }
}

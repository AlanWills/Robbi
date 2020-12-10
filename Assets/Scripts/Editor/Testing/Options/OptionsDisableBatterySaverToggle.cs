using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsDisableBatterySaverToggle : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Disable Battery Saver Toggle")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsDisableBatterySaverToggle>();
        }
    }
}

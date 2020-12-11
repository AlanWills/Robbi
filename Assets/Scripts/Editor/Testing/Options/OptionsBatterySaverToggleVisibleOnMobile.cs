using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsBatterySaverToggleVisibleOnMobile : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Battery Saver Toggle Visible On Mobile")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsBatterySaverToggleVisibleOnMobile>();
        }
    }
}

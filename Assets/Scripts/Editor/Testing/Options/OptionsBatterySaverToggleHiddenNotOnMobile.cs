using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsBatterySaverToggleHiddenNotOnMobile : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Battery Saver Toggle Hidden Not On Mobile")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsBatterySaverToggleHiddenNotOnMobile>();
        }
    }
}

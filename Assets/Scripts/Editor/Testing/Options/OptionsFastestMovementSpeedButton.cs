using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsFastestMovementSpeedButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Fastest Movement Speed Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsFastestMovementSpeedButton>();
        }
    }
}

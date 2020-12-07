using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsNormalMovementSpeedButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Normal Movement Speed Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsNormalMovementSpeedButton>();
        }
    }
}

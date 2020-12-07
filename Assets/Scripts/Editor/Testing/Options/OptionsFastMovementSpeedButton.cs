using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsFastMovementSpeedButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Fast Movement Speed Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsFastMovementSpeedButton>();
        }
    }
}

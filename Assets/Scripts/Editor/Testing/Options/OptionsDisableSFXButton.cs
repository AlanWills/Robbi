using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsDisableSFXButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Disable SFX Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsDisableSFXButton>();
        }
    }
}

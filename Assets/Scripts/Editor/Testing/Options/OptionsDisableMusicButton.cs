using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsDisableMusicButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Disable Music Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsDisableMusicButton>();
        }
    }
}

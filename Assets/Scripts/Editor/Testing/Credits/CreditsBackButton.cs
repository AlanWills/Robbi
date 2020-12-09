using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CreditsBackButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Credits/Credits Back Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CreditsBackButton>();
        }
    }
}

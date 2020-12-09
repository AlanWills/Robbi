using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class MainMenuToCredits : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Main Menu/Main Menu To Credits")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<MainMenuToCredits>();
        }
    }
}

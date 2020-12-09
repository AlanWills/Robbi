using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class MainMenuToOptions : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Main Menu/Main Menu To Options")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<MainMenuToOptions>();
        }
    }
}

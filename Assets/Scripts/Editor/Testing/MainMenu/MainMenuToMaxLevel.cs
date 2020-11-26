using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class MainMenuToMaxLevel
    {
        [MenuItem("Robbi/Testing/Main Menu/Main Menu To Max Level")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<MainMenuToMaxLevel>();
        }
    }
}

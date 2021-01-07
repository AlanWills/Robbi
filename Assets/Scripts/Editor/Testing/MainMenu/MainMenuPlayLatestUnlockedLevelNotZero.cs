using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class MainMenuPlayLatestUnlockedLevelNotZero : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Main Menu/Main Menu Play Latest Unlocked Level Not Zero")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<MainMenuPlayLatestUnlockedLevelNotZero>();
        }
    }
}

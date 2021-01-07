using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class UsingInteractBoosterReducesBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Using Interact Booster Reduces Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<UsingInteractBoosterReducesBalance>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CannotClickOnInteractBoosterWhenBoosterUnusable : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Cannot Click On Interact Booster When Booster Unusable")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CannotClickOnInteractBoosterWhenBoosterUnusable>();
        }
    }
}

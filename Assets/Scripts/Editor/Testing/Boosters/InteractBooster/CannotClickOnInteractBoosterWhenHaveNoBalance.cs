using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CannotClickOnInteractBoosterWhenHaveNoBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Cannot Click On Interact Booster When Have No Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CannotClickOnInteractBoosterWhenHaveNoBalance>();
        }
    }
}

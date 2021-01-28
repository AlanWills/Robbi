using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CannotClickOnDoorToggleBoosterWhenBoosterUnusable : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Cannot Click On Door Toggle Booster When Booster Unusable")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CannotClickOnDoorToggleBoosterWhenBoosterUnusable>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CannotClickOnDoorToggleBoosterWhenHaveNoBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Cannot Click On Door Toggle Booster When Have No Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CannotClickOnDoorToggleBoosterWhenHaveNoBalance>();
        }
    }
}

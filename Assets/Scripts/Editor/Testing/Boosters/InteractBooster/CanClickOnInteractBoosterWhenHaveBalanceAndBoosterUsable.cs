using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CanClickOnInteractBoosterWhenHaveBalanceAndBoosterUsable : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Can Click On Interact Booster When Have Balance And Booster Usable")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CanClickOnInteractBoosterWhenHaveBalanceAndBoosterUsable>();
        }
    }
}

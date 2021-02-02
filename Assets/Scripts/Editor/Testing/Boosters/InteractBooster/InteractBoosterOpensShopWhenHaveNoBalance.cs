using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class InteractBoosterOpensShopWhenHaveNoBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Interact Booster/Interact Booster Opens Shop When Have No Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<InteractBoosterOpensShopWhenHaveNoBalance>();
        }
    }
}

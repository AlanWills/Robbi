using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class BuyingItemUpdatesPurchaseabilityOfOtherItems : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Shop/Buying Item Updates Purchaseability Of Other Items")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<BuyingItemUpdatesPurchaseabilityOfOtherItems>();
        }
    }
}

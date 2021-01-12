using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WithEnoughMoneyCanPurchaseUnlockedItem : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Shop/With Enough Money Can Purchase Unlocked Item")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WithEnoughMoneyCanPurchaseUnlockedItem>();
        }
    }
}

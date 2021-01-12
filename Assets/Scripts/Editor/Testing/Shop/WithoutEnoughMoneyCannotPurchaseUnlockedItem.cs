using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WithoutEnoughMoneyCannotPurchaseUnlockedItem : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Shop/Without Enough Money Cannot Purchase Unlocked Item")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WithoutEnoughMoneyCannotPurchaseUnlockedItem>();
        }
    }
}

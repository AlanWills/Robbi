using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class ShopBackButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Shop/Shop Back Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<ShopBackButton>();
        }
    }
}

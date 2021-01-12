﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class ItemLockedCannotBePurchased : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Shop/Item Locked Cannot Be Purchased")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<ItemLockedCannotBePurchased>();
        }
    }
}

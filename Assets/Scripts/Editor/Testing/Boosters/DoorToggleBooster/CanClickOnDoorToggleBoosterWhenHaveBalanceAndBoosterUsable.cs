﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class CanClickOnDoorToggleBoosterWhenHaveBalanceAndBoosterUsable : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Door Toggle Booster/Can Click On Door Toggle Booster When Have Balance And Booster Usable")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<CanClickOnDoorToggleBoosterWhenHaveBalanceAndBoosterUsable>();
        }
    }
}

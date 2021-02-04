﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class WaypointBoosterOpensShopWhenHaveNoBalance : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Boosters/Waypoint Booster/Waypoint Booster Opens Shop When Have No Balance")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<WaypointBoosterOpensShopWhenHaveNoBalance>();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class LevelShopButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Shop Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelShopButton>();
        }
    }
}

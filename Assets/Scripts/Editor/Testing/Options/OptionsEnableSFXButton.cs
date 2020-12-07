﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public class OptionsEnableSFXButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Options/Options Enable SFX Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<OptionsEnableSFXButton>();
        }
    }
}

﻿using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using CelesteEditor.Tools;

namespace RobbiEditor.Testing
{
    public class LevelLoseOutOfFuelMainMenuButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Out Of Fuel/Level Lose Out Of Fuel Main Menu Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfFuelMainMenuButton>();
        }
    }
}
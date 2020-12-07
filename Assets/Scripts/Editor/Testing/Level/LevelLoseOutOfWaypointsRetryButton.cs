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
using RobbiEditor.Utils;

namespace RobbiEditor.Testing
{
    public class LevelLoseOutOfWaypointsRetryButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Lose Out Of Waypoints Retry Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfWaypointsRetryButton>();
        }
    }
}

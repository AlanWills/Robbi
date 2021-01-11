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
    public class LevelLoseOutOfWaypoints : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Out Of Waypoints/Level Lose Out Of Waypoints")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfWaypoints>();
        }
    }
}
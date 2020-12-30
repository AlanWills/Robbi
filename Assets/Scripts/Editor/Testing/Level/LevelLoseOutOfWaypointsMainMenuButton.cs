using Robbi.Testing;
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
    public class LevelLoseOutOfWaypointsMainMenuButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Level Lose Out Of Waypoints Main Menu Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseOutOfWaypointsMainMenuButton>();
        }
    }
}

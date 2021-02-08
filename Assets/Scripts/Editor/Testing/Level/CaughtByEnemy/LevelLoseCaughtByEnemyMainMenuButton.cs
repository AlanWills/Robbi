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
    public class LevelLoseCaughtByEnemyMainMenuButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Caught By Enemy/Level Lose Caught By Enemy Main Menu Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseCaughtByEnemyMainMenuButton>();
        }
    }
}

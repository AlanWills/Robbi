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
    public class LevelLoseMissingCollectionTargetMainMenuButton : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Missing Collection Target/Level Lose Missing Collection Target Main Menu Button")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseMissingCollectionTargetMainMenuButton>();
        }
    }
}

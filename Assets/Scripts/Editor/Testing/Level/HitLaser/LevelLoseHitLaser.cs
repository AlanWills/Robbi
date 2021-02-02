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
    public class LevelLoseHitLaser : IIntegrationTest
    {
        [MenuItem("Robbi/Testing/Level/Hit Laser/Level Lose Hit Laser")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTest<LevelLoseHitLaser>();
        }
    }
}

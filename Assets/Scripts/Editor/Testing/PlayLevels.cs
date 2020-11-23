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

namespace RobbiEditor.Testing
{
    [InitializeOnLoad]
    public static class PlayLevels
    {
        [MenuItem("Robbi/Testing/Play Levels")]
        public static void MenuItem()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Splash.unity", OpenSceneMode.Single);
            IntegrationTestRunner.Instance.RunTest("PlayLevels");
        }
    }
}

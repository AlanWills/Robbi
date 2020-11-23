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

namespace RobbiEditor
{
    [InitializeOnLoad]
    public static class EditorStartup
    {
        private const string STARTUP_SCENE_NAME = "Splash";
        private const string STARTUP_SCENE_PATH = "Assets/Scenes/Splash.unity";

        static EditorStartup()
        {
            EditorApplication.update += OnEditorOpened;
            EditorApplication.playModeStateChanged += (PlayModeStateChange obj) =>
            {
                if (obj == PlayModeStateChange.ExitingEditMode)
                {
                    OpenStartupScene();
                }
                else if (obj == PlayModeStateChange.EnteredEditMode)
                {
                    ClearIntegrationTest();
                }
            };
        }

        private static void OnEditorOpened()
        {
            EditorApplication.update -= OnEditorOpened;
            OpenStartupScene();
            ClearIntegrationTest();
            AssetDatabase.SaveAssets();
        }

        private static void OpenStartupScene()
        {
            if (SceneManager.GetActiveScene().name != STARTUP_SCENE_NAME)
            {
                Debug.Log("Setting active scene to " + STARTUP_SCENE_NAME);
                EditorSceneManager.OpenScene(STARTUP_SCENE_PATH, OpenSceneMode.Single);
            }
        }

        private static void ClearIntegrationTest()
        {
            IntegrationTestRunner.Instance.ClearTest();
        }
    }
}

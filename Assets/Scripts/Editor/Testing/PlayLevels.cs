using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing
{
    public class PlayLevels
    {
        private static PlayLevels instance;
        private static PlayLevels Instance
        {
            get
            {
                instance = instance ?? new PlayLevels();
                return instance;
            }
        }

        [MenuItem("Robbi/Testing/Play Levels")]
        public static void MenuItem()
        {
            EditorApplication.playModeStateChanged += Instance.EditorApplication_playModeStateChanged;
            EditorApplication.EnterPlaymode();
        }

        private void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.EnteredPlayMode)
            {
                GameObject integrationTestRunner = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Testing/IntegrationTestRunner.prefab");
                GameObject testRunnerInstance = GameObject.Instantiate(integrationTestRunner);
                testRunnerInstance.GetComponent<IntegrationTestRunner>().integrationTestName = "PlayLevels";
                testRunnerInstance.SetActive(true);

                EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
            }
        }
    }
}

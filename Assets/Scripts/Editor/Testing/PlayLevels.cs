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
        [MenuItem("Robbi/Testing/Play Levels")]
        public static void MenuItem()
        {
            GameObject integrationTestRunner = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Testing/IntegrationTestRunner.prefab");
            GameObject testRunnerInstance = GameObject.Instantiate(integrationTestRunner);
            IntegrationTestRunner runner = testRunnerInstance.GetComponent<IntegrationTestRunner>();
            runner.RunTest("PlayLevels");
        }
    }
}

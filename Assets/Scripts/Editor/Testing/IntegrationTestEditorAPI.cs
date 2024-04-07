using Celeste.Testing;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing
{
    public static class IntegrationTestEditorAPI
    {
        public static void RunTests(List<IntegrationTest> tests)
        {
            EditorApplication.EnterPlaymode();
            //EditorApplication.playModeStateChanged += (PlayModeStateChange obj) =>
            //{
            //    if (obj == PlayModeStateChange.EnteredPlayMode)
            //    {
            //        IntegrationTestRunner integrationTestRunner = GameObject.FindFirstObjectByType<IntegrationTestRunner>();
            //        GameObject.DontDestroyOnLoad(integrationTestRunner);
            //        integrationTestRunner.RunTests(tests);
            //    }
            //};
            //EditorStartup.OpenStartupScene();
        }

        public static void RunTests(IEnumerable<string> testNames)
        {
            //EditorStartup.OpenStartupScene();
            //GameObject.FindFirstObjectByType<IntegrationTestRunner>().RunTests()
            //IntegrationTestRunner.Instance.RunTests(testNames);
        }

        public static void RunTests(params Type[] testTypes)
        {
            List<string> testNames = new List<string>(testTypes.Length);
            for (int i = 0; i < testTypes.Length; ++i)
            {
                testNames.Add(testTypes[i].Name);
            }

            RunTests(testNames);
        }

        public static void RunTest(string testName)
        {
            RunTests(new List<string>() { testName });
        }

        public static void RunTest<T>()
        {
            RunTest(typeof(T).Name);
        }
    }
}

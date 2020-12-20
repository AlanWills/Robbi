using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing
{
    public static class RunAllIntegrationTests
    {
        [MenuItem("Robbi/Testing/Find All Integration Tests")]
        public static void FindMenuItem()
        {
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();
            List<Type> integrationTests = FindIntegrationTests();

            stopWatch.Stop();
            Debug.LogFormat("Found {0} Integration Tests in {1} seconds", integrationTests.Count, stopWatch.ElapsedMilliseconds / 1000.0f);

            foreach (Type iTest in integrationTests)
            {
                Debug.LogFormat("Found Integration Test: {0}", iTest.Name);
            }
        }

        [MenuItem("Robbi/Testing/Run All Integration Tests")]
        public static void RunMenuItem()
        {
            IntegrationTestEditorAPI.RunTests(FindIntegrationTests().ToArray());
        }

        private static List<Type> FindIntegrationTests()
        {
            Type integrationTestType = typeof(IIntegrationTest);
            List<Type> integrationTests = new List<Type>();

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (integrationTestType.IsAssignableFrom(t) && !t.IsAbstract)
                    {
                        integrationTests.Add(t);
                    }
                }
            }

            return integrationTests;
        }
    }
}

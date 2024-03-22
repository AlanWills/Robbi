using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace RobbiEditor.Testing
{
    public static class IntegrationTests
    {
        #region Properties and Fields

        public static readonly List<Type> AllIntegrationTests = new List<Type>();

        #endregion

        static IntegrationTests()
        {
            System.Diagnostics.Stopwatch stopWatch = System.Diagnostics.Stopwatch.StartNew();

            AllIntegrationTests.Clear();
            Type integrationTestType = typeof(IIntegrationTest);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type t in assembly.GetTypes())
                {
                    if (integrationTestType.IsAssignableFrom(t) && !t.IsAbstract)
                    {
                        AllIntegrationTests.Add(t);
                    }
                }
            }

            stopWatch.Stop();
            Debug.LogFormat("Loaded {0} Integration Tests in {1} seconds", AllIntegrationTests.Count, stopWatch.ElapsedMilliseconds / 1000.0f);
        }
    }
}

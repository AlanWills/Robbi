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
        [MenuItem("Robbi/Testing/Run All Integration Tests")]
        public static void MenuItem()
        {
            List<Type> integrationTests = new List<Type>();
            Type integrationTestType = typeof(IIntegrationTest);

            foreach (Type t in Assembly.GetAssembly(integrationTestType).GetTypes())
            {
                if (integrationTestType.IsAssignableFrom(t) && !t.IsAbstract)
                {
                    integrationTests.Add(t);
                }
            }

            IntegrationTestEditorAPI.RunTests(integrationTests.ToArray());
        }
    }
}

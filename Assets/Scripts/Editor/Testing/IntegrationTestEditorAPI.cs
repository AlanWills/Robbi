using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Testing
{
    public static class IntegrationTestEditorAPI
    {
        public static void RunTests(IEnumerable<string> testNames)
        {
            EditorStartup.OpenStartupScene();
            IntegrationTestRunner.Instance.RunTests(testNames);
        }

        public static void RunTest(string testName)
        {
            RunTests(new List<string>() { testName });
        }

        public static void RunTest<T>()
        {
            RunTest(typeof(T).Name);
        }

        [MenuItem("Robbi/Test/Test Multiple")]
        public static void MenuItem()
        {
            IntegrationTestEditorAPI.RunTests(new List<string>() { "MainMenuToLevel", "MainMenuToLockedLevel" });
        }
    }
}

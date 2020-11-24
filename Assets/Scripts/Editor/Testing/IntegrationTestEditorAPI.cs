using Robbi.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Testing
{
    public static class IntegrationTestEditorAPI
    {
        public static void RunTest(string testName)
        {
            EditorStartup.OpenStartupScene();
            IntegrationTestRunner.Instance.RunTest(testName);
        }

        public static void RunTest<T>()
        {
            RunTest(typeof(T).Name);
        }
    }
}

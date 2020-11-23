using Robbi.Attributes.GUI;
using Robbi.Events;
using Robbi.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Testing
{
    [AddComponentMenu("Robbi/Testing/Integration Test Runner")]
    public class IntegrationTestRunner : MonoBehaviour
    {
        #region Properties and Fields

        public StringEvent executeConsoleCommand;

        [SerializeField, ReadOnlyAtRuntime]
        private string integrationTestName = "";
        private Coroutine testCoroutine;

        public static IntegrationTestRunner Instance
        {
            get { return GameObject.Find("IntegrationTestRunner").GetComponent<IntegrationTestRunner>(); }
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (!string.IsNullOrEmpty(integrationTestName))
            {
                testCoroutine = StartCoroutine(RunTestImpl());
            }
            else
            {
                Debug.LogError("No integration test name specified for IntegrationTestRunner");
            }
        }

        #endregion

        #region Testing Methods

        public void RunTest(string integrationTestName)
        {
            this.integrationTestName = integrationTestName;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.EditorApplication.EnterPlaymode();
#endif
        }

        private IEnumerator RunTestImpl()
        {
#if UNITY_EDITOR
            while (!UnityEditor.EditorApplication.isPlaying) { yield return null; }

            // Can only do this in Play Mode
            DontDestroyOnLoad(this);

            yield return new WaitForSeconds(10);

            executeConsoleCommand.Raise("it " + integrationTestName);
#endif
        }

        public void TryPassTest(string testName)
        {
            if (testName == integrationTestName)
            {
                Exit(true);
            }
        }

        public void TryFailTest(string testName)
        {
            if (testName == integrationTestName)
            {
                Exit(false);
            }
        }

#endregion

#region Results

        private void Exit(bool testResult)
        {
            StopCoroutine(testCoroutine);
            testCoroutine = null;

#if UNITY_EDITOR
            string directoryPath = Path.Combine(Application.dataPath, "..", "TestResults");
            string testResultString = testResult ? "Passed" : "Failed";

            Debug.LogFormat("Integration Test {0}: {1}", integrationTestName, testResultString);

            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(
                Path.Combine(directoryPath, string.Format("{0}-{1}.txt", integrationTestName, testResultString)), 
                testResult ? "1" : "0");

            // 0 = everything OK
            // 1 = everything NOT OK
            UnityEditor.EditorApplication.Exit(testResult ? 0 : 1);
#endif
        }

#endregion
    }
}

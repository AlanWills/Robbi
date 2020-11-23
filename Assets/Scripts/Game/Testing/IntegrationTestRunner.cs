using Robbi.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Robbi.Testing
{
    [AddComponentMenu("Robbi/Testing/Integration Test Runner")]
    public class IntegrationTestRunner : MonoBehaviour
    {
        #region Properties and Fields

        public string integrationTestName;
        public StringEvent executeConsoleCommand;

        private Coroutine testCoroutine;

        #endregion

        #region Testing Methods

        public void RunTest()
        {
            testCoroutine = StartCoroutine(RunTestImpl());
        }

        private IEnumerator RunTestImpl()
        {
            Debug.Assert(!string.IsNullOrEmpty(integrationTestName), "Integration Test Name is not set on runner");

            // Can only do this in Play Mode
            DontDestroyOnLoad(this);

            yield return new WaitForSeconds(10);

            executeConsoleCommand.Raise("it " + integrationTestName);
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
            gameObject.SetActive(false);

            string directoryPath = Path.Combine(Application.dataPath, "..", "TestResults");
            string testResultString = testResult ? "Passed" : "Failed";

            Debug.LogFormat("Integration Test {0}: {1}", integrationTestName, testResultString);

            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(
                Path.Combine(directoryPath, string.Format("{0}-{1}.txt", integrationTestName, testResultString)), 
                testResult ? "1" : "0");

            EditorApplication.ExitPlaymode();
        }

        #endregion
    }
}

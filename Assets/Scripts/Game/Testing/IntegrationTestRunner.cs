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
        private StringBuilder logContents = new StringBuilder(1024 * 1024);

        public static IntegrationTestRunner Instance
        {
            get { return GameObject.Find("IntegrationTestRunner").GetComponent<IntegrationTestRunner>(); }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (!string.IsNullOrEmpty(integrationTestName))
            {
                testCoroutine = StartCoroutine(RunTestImpl());
            }
            else
            {
                Debug.Log("No integration test name specified for IntegrationTestRunner");
            }
        }

        private void Start()
        {
            if (testCoroutine == null)
            {
                gameObject.SetActive(false);
            }
        }

        #endregion

        #region Testing Methods

        public void RunTest(string integrationTestName)
        {
            this.integrationTestName = integrationTestName;
            gameObject.SetActive(true);

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.EditorApplication.EnterPlaymode();
#endif
        }

        public void ClearTest()
        {
            integrationTestName = "";

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }

        private IEnumerator RunTestImpl()
        {
#if UNITY_EDITOR
            while (!UnityEditor.EditorApplication.isPlaying) { yield return null; }

            Application.logMessageReceived += (string logString, string stackTrace, LogType type) =>
            {
                logContents.AppendLine(logString);
            };

            // Can only do this in Play Mode
            DontDestroyOnLoad(this);

            // Disable audio since this will be run via CI
            AudioListener.volume = 0;

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
            gameObject.SetActive(false);

#if UNITY_EDITOR
            string directoryPath = Path.Combine(Application.dataPath, "..", "TestResults");

            Directory.CreateDirectory(directoryPath);
            File.WriteAllText(
                Path.Combine(directoryPath, string.Format("{0}-{1}.txt", integrationTestName, testResult ? "Passed" : "Failed")),
                (testResult ? "1\n" : "0\n") + logContents.ToString());

            if (Application.isBatchMode)
            {
                // 0 = everything OK
                // 1 = everything NOT OK
                UnityEditor.EditorApplication.Exit(testResult ? 0 : 1);
            }
            else
            {
                UnityEditor.EditorApplication.ExitPlaymode();
            }
#endif
        }

#endregion
    }
}

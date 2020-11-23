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
    [ExecuteInEditMode]
    public class IntegrationTestRunner : MonoBehaviour
    {
        #region Properties and Fields

        public StringEvent executeConsoleCommand;

        [SerializeField, HideInInspector]
        private string integrationTestName;
        private Coroutine testCoroutine;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            //EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;

            //if (!string.IsNullOrEmpty(integrationTestName))
            //{
            //    testCoroutine = StartCoroutine(RunTestImpl());
            //}
            //else
            //{
            //    GameObject.DestroyImmediate(gameObject);
            //    Debug.LogError("Deleting invalid IntegrationTestRunner");
            //}
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;
        }

        private void EditorApplication_playModeStateChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                integrationTestName = "";
            }

            if (obj == PlayModeStateChange.EnteredEditMode)
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }

        #endregion

        #region Testing Methods

        public void RunTest(string integrationTestName)
        {
            this.integrationTestName = integrationTestName;
            gameObject.SetActive(true);
        }

        private IEnumerator RunTestImpl()
        {
            EditorApplication.EnterPlaymode();

            while (!EditorApplication.isPlaying) { yield return null; }

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
            integrationTestName = "";
            StopCoroutine(testCoroutine);
            testCoroutine = null;

            GameObject.DestroyImmediate(gameObject);

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

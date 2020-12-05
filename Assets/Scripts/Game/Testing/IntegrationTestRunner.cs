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
using UnityEngine.SceneManagement;

namespace Robbi.Testing
{
    [AddComponentMenu("Robbi/Testing/Integration Test Runner")]
    public class IntegrationTestRunner : MonoBehaviour
    {
        #region Properties and Fields

        public StringEvent executeConsoleCommand;

        [SerializeField, ReadOnlyAtRuntime]
        private List<string> integrationTestNames;

        [SerializeField, ReadOnlyAtRuntime]
        private int currentTestIndex;

        private Coroutine testCoroutine;
        private bool testInProgress;
        private bool testResult;
        private StringBuilder logContents = new StringBuilder(1024 * 1024);

        public static IntegrationTestRunner Instance
        {
            get { return GameObject.Find("IntegrationTestRunner").GetComponent<IntegrationTestRunner>(); }
        }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            if (integrationTestNames.Count > 0)
            {
                testCoroutine = StartCoroutine(RunTestImpl());
            }
            else
            {
                Debug.Log("No integration test names specified for IntegrationTestRunner");
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

        public void RunTest(string testName)
        {
            RunTests(new List<string>() { testName });
        }

        public void RunTests(IEnumerable<string> testNames)
        {
            integrationTestNames.Clear();
            integrationTestNames.AddRange(testNames);
            currentTestIndex = 0;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.EditorApplication.EnterPlaymode();
#endif
        }

        public void ClearTests()
        {
            if (currentTestIndex >= integrationTestNames.Count && currentTestIndex > 0)
            {
                Debug.LogWarning("Clearing integration tests");

                integrationTestNames.Clear();
                currentTestIndex = 0;

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssets();
#endif
            }
            else
            {
                Debug.LogWarning("Skipped clearing integration tests");
            }
        }

        private IEnumerator RunTestImpl()
        {
#if UNITY_EDITOR
            testInProgress = true;
            testResult = false;

            string testResultsDirectory = Path.Combine(Application.dataPath, "..", "TestResults");
            if (Directory.Exists(testResultsDirectory))
            {
                Directory.Delete(testResultsDirectory, true);
            }

            while (!UnityEditor.EditorApplication.isPlaying) { yield return null; }

            Application.logMessageReceived += (string logString, string stackTrace, LogType type) =>
            {
                logContents.AppendLine(logString);
            };

            // Can only do this in Play Mode
            DontDestroyOnLoad(this);

            // Disable audio since this will be run via CI
            AudioListener.volume = 0;

            bool sceneFound = false;
            while (!sceneFound)
            {
                Scene scene = SceneManager.GetSceneByName("MainMenu");
                sceneFound = scene != null && scene.IsValid();

                yield return null;
            }

            // Dunno why, but just waiting a second here seems to do the world of good
            yield return new WaitForSeconds(1);

            executeConsoleCommand.Raise("it " + integrationTestNames[currentTestIndex]);

            while (testInProgress) { yield return null; }

            Directory.CreateDirectory(testResultsDirectory);
            File.WriteAllText(
                Path.Combine(testResultsDirectory, string.Format("{0}-{1}.txt", integrationTestNames[currentTestIndex], testResult ? "Passed" : "Failed")),
                (testResult ? "1\n" : "0\n") + logContents.ToString());

            UnityEditor.EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
            UnityEditor.EditorApplication.ExitPlaymode();

#else
            return null;
#endif
        }

#if UNITY_EDITOR
        private static void EditorApplication_playModeStateChanged(UnityEditor.PlayModeStateChange obj)
        {
            if (obj == UnityEditor.PlayModeStateChange.EnteredEditMode)
            {
                IntegrationTestRunner instance = Instance;

                ++instance.currentTestIndex;
                UnityEditor.EditorApplication.playModeStateChanged -= EditorApplication_playModeStateChanged;

                if (instance.currentTestIndex < instance.integrationTestNames.Count)
                {
                    UnityEditor.EditorUtility.SetDirty(instance);
                    UnityEditor.AssetDatabase.SaveAssets();
                    UnityEditor.EditorApplication.EnterPlaymode();
                }
                else
                {
                    bool testRunSuccessful = WasTestRunSuccessful(instance.integrationTestNames);

                    instance.ClearTests();
                    
                    UnityEditor.EditorUtility.SetDirty(instance);
                    UnityEditor.AssetDatabase.SaveAssets();

                    if (Application.isBatchMode)
                    {
                        // 0 = everything OK
                        // 1 = everything NOT OK
                        UnityEditor.EditorApplication.Exit(testRunSuccessful ? 0 : 1);
                    }
                    else
                    {
                        UnityEditor.EditorUtility.DisplayDialog("Test Results", string.Format("Tests {0}", testRunSuccessful ? "Passed" : "Failed"), "OK");
                        UnityEditor.EditorApplication.ExitPlaymode();
                    }
                }
            }
        }

        private static bool WasTestRunSuccessful(List<string> integrationTestNames)
        {
            string directoryPath = Path.Combine(Application.dataPath, "..", "TestResults");
            foreach (string testName in integrationTestNames)
            {
                if (File.Exists(Path.Combine(directoryPath, testName + "-Failed.txt")))
                {
                    return false;
                }
            }

            return true;
        }
#endif

        public void TryPassTest(string testName)
        {
            TrySetResult(testName, true);
        }

        public void TryFailTest(string testName)
        {
            TrySetResult(testName, false);
        }

        private void TrySetResult(string testName, bool result)
        {
            if (testName == integrationTestNames[currentTestIndex])
            {
                testInProgress = false;
                testResult = result;
            }
        }

#endregion
    }
}

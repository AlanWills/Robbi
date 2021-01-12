using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing.GUI
{
    public class IntegrationTestWizard : ScriptableWizard
    {
        #region Properties and Fields

        private List<IntegrationTestGUI> testGUIs = new List<IntegrationTestGUI>();
        private Vector2 scrollPosition;
        private TestPlaylist testPlaylist;

        #endregion

        #region GUI

        private void OnEnable()
        {
            RefreshGUI();
        }

        protected override bool DrawWizardGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Select All", GUILayout.ExpandWidth(false)))
            {
                SetSelectionStatusForAll(true);
            }

            if (GUILayout.Button("Deselect All", GUILayout.ExpandWidth(false)))
            {
                SetSelectionStatusForAll(false);
            }

            if (GUILayout.Button("Refresh", GUILayout.ExpandWidth(false)))
            {
                RefreshGUI();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            testPlaylist = EditorGUILayout.ObjectField(testPlaylist, typeof(TestPlaylist), false) as TestPlaylist;
            if (EditorGUI.EndChangeCheck())
            {
                SetSelectionStatusForAll(false);

                if (testPlaylist != null)
                {
                    HashSet<string> playlistNames = testPlaylist.IntegrationTestNames;
                    foreach (IntegrationTestGUI testGui in testGUIs)
                    {
                        if (playlistNames.Contains(testGui.TestName))
                        {
                            testGui.IsSelected = true;
                        }
                    }
                }
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            foreach (IntegrationTestGUI testGUI in testGUIs)
            {
                testGUI.GUI();
            }

            EditorGUILayout.EndScrollView();

            return true;
        }

        private void OnWizardCreate() { }

        private void OnWizardOtherButton()
        {
            List<string> selectedTests = new List<string>();

            foreach (IntegrationTestGUI testGui in testGUIs)
            {
                if (testGui.IsSelected)
                {
                    selectedTests.Add(testGui.TestName);
                }
            }

            IntegrationTestEditorAPI.RunTests(selectedTests);
        }

        #endregion

        #region Utility Methods

        private void SetSelectionStatusForAll(bool selectionStatus)
        {
            foreach (IntegrationTestGUI testGUI in testGUIs)
            {
                testGUI.IsSelected = selectionStatus;
            }
        }

        private void RefreshGUI()
        {
            testGUIs.Clear();

            foreach (Type test in IntegrationTests.AllIntegrationTests.OrderBy(x => x.Name))
            {
                string testResultsDirectory = Path.Combine(Application.dataPath, "..", "TestResults");
                TestResult result = TestResult.NotRun;

                if (File.Exists(Path.Combine(testResultsDirectory, test.Name + "-Failed.txt")))
                {
                    result = TestResult.Failed;
                }
                else if (File.Exists(Path.Combine(testResultsDirectory, test.Name + "-Passed.txt")))
                {
                    result = TestResult.Passed;
                }

                testGUIs.Add(new IntegrationTestGUI(test, result));
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Robbi/Testing/Integration Test Wizard")]
        public static void ShowIntegrationTestWizard()
        {
            ScriptableWizard.DisplayWizard<IntegrationTestWizard>("Run Integration Tests", "Close", "Run Selected");
        }

        #endregion
    }
}

using Celeste.FSM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Testing
{
    [CreateAssetMenu(fileName = "TestPlaylist", menuName = "Robbi/Testing/Test Playlist")]
    public class TestPlaylist : ScriptableObject
    {
        #region Properties and Fields

        public HashSet<string> IntegrationTestNames
        {
            get
            {
                HashSet<string> testNames = new HashSet<string>();
                foreach (FSMGraph fsmGraph in integrationTests)
                {
                    testNames.Add(fsmGraph.name);
                }

                return testNames;
            }
        }

        [SerializeField] private List<FSMGraph> integrationTests = new List<FSMGraph>();

        #endregion
    }

    [CustomEditor(typeof(TestPlaylist))]
    public class TestPlaylistEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Find Tests", GUILayout.ExpandWidth(false)))
            {
                string location = AssetDatabase.GetAssetPath(target);
                string parentDirectory = location.Substring(0, location.LastIndexOf('/'));
                string[] integrationTestGuids = AssetDatabase.FindAssets("t:FSMGraph", new string[] { parentDirectory });

                serializedObject.Update();

                SerializedProperty integrationTestsProperty = serializedObject.FindProperty("integrationTests");
                integrationTestsProperty.arraySize = integrationTestGuids.Length;

                for (int i = 0; i < integrationTestGuids.Length; ++i)
                {
                    string integrationTestPath = AssetDatabase.GUIDToAssetPath(integrationTestGuids[i]);
                    FSMGraph integrationTest = AssetDatabase.LoadAssetAtPath<FSMGraph>(integrationTestPath);

                    SerializedProperty arrayElement = integrationTestsProperty.GetArrayElementAtIndex(i);
                    arrayElement.objectReferenceValue = integrationTest;
                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        #endregion
    }
}
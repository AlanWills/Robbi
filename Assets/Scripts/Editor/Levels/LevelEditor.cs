using Robbi.Levels;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            if (GUILayout.Button("Find Interactables"))
            {
                string location = AssetDatabase.GetAssetPath(target);
                string interactablesFolder = string.Format("{0}/{1}", Path.GetDirectoryName(location).Replace('\\', '/'), LevelDirectories.INTERACTABLES_NAME);
                interactablesFolder = interactablesFolder.EndsWith("/") ? interactablesFolder.Substring(0, interactablesFolder.Length - 1) : interactablesFolder;
                string[] scriptableObjectGuids = AssetDatabase.FindAssets("t:ScriptableObject", new string[] { interactablesFolder });

                List<ScriptableObject> interactables = new List<ScriptableObject>();
                foreach (string scriptableObjectGuid in scriptableObjectGuids)
                {
                    string scriptableObjectPath = AssetDatabase.GUIDToAssetPath(scriptableObjectGuid);
                    ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(scriptableObjectPath);

                    if (scriptableObject is IInteractable)
                    {
                        interactables.Add(scriptableObject);
                    }
                }

                SerializedProperty interactablesProperty = serializedObject.FindProperty("interactables");
                interactablesProperty.arraySize = interactables.Count;

                for (int i = 0; i < interactables.Count; ++i)
                {
                    interactablesProperty.GetArrayElementAtIndex(i).objectReferenceValue = interactables[i];
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Utility Methods

        public static void FindInteractables(Level level)
        {
            string location = AssetDatabase.GetAssetPath(level);
            string interactablesFolder = string.Format("{0}/{1}", Path.GetDirectoryName(location).Replace('\\', '/'), LevelDirectories.INTERACTABLES_NAME);
            interactablesFolder = interactablesFolder.EndsWith("/") ? interactablesFolder.Substring(0, interactablesFolder.Length - 1) : interactablesFolder;
            string[] scriptableObjectGuids = AssetDatabase.FindAssets("t:ScriptableObject", new string[] { interactablesFolder });

            List<ScriptableObject> interactables = new List<ScriptableObject>();
            foreach (string scriptableObjectGuid in scriptableObjectGuids)
            {
                string scriptableObjectPath = AssetDatabase.GUIDToAssetPath(scriptableObjectGuid);
                ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(scriptableObjectPath);

                if (scriptableObject is IInteractable)
                {
                    interactables.Add(scriptableObject);
                }
            }

            SerializedObject serializedObject = new SerializedObject(level);
            serializedObject.Update();

            SerializedProperty interactablesProperty = serializedObject.FindProperty("interactables");
            bool dirty = interactablesProperty.arraySize != interactables.Count;

            interactablesProperty.arraySize = interactables.Count;

            for (int i = 0; i < interactables.Count; ++i)
            {
                SerializedProperty arrayElement = interactablesProperty.GetArrayElementAtIndex(i);

                if (interactables[i] != arrayElement.objectReferenceValue)
                {
                    arrayElement.objectReferenceValue = interactables[i];
                    dirty = true;
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (dirty)
            {
                EditorUtility.SetDirty(level);
            }
        }

        #endregion
    }
}

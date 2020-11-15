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
                string[] interactableGuids = AssetDatabase.FindAssets("t:Interactable", new string[] { interactablesFolder });

                SerializedProperty interactables = serializedObject.FindProperty("interactables");
                interactables.arraySize = interactableGuids.Length;

                for (int i = 0; i < interactableGuids.Length; ++i)
                {
                    string interactablePath = AssetDatabase.GUIDToAssetPath(interactableGuids[i]);
                    interactables.GetArrayElementAtIndex(i).objectReferenceValue = AssetDatabase.LoadAssetAtPath<Interactable>(interactablePath);
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
            string[] interactableGuids = AssetDatabase.FindAssets("t:Interactable", new string[] { interactablesFolder });

            SerializedObject serializedObject = new SerializedObject(level);
            serializedObject.Update();

            SerializedProperty interactables = serializedObject.FindProperty("interactables");
            interactables.arraySize = interactableGuids.Length;

            for (int i = 0; i < interactableGuids.Length; ++i)
            {
                string interactablePath = AssetDatabase.GUIDToAssetPath(interactableGuids[i]);
                interactables.GetArrayElementAtIndex(i).objectReferenceValue = AssetDatabase.LoadAssetAtPath<Interactable>(interactablePath);
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(level);
        }

        #endregion
    }
}

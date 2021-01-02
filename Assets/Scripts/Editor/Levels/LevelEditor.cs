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
        #region Properties and Fields

        private SerializedProperty requiresFuelProperty;
        private SerializedProperty startingFuelProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            requiresFuelProperty = serializedObject.FindProperty("requiresFuel");
            startingFuelProperty = serializedObject.FindProperty("startingFuel");
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Level level = target as Level;

            DrawPropertiesExcluding(serializedObject, "m_Script", "requiresFuel", "startingFuel");

            EditorGUILayout.PropertyField(requiresFuelProperty);
            if (requiresFuelProperty.boolValue)
            {
                EditorGUILayout.PropertyField(startingFuelProperty);
            }

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Find Portals", GUILayout.ExpandWidth(false)))
            {
                FindPortals(level);
            }

            if (GUILayout.Button("Find Doors", GUILayout.ExpandWidth(false)))
            {
                FindDoors(level);
            }

            if (GUILayout.Button("Find Interactables", GUILayout.ExpandWidth(false)))
            {
                FindInteractables(level);
            }

            if (GUILayout.Button("Find Collectables", GUILayout.ExpandWidth(false)))
            {
                FindCollectables(level);
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Utility Methods

        public static void FindAllLevelObjects(Level level)
        {
            FindPortals(level);
            FindDoors(level);
            FindInteractables(level);
            FindCollectables(level);
        }

        private static void FindPortals(Level level)
        {
            Find<Portal>(level, LevelDirectories.PORTALS_NAME, "portals");
        }

        private static void FindDoors(Level level)
        {
            Find<Door>(level, LevelDirectories.DOORS_NAME, "doors");
        }

        private static void FindInteractables(Level level)
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
            bool dirty = false;
            
            if (interactablesProperty.arraySize != interactables.Count)
            {
                interactablesProperty.arraySize = interactables.Count;
                dirty = true;
            }

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

        private static void FindCollectables(Level level)
        {
            Find<Collectable>(level, LevelDirectories.COLLECTABLES_NAME, "collectables");
        }

        private static void Find<T>(Level level, string directoryName, string propertyName) where T : ScriptableObject
        {
            string location = AssetDatabase.GetAssetPath(level);
            string objectFolder = string.Format("{0}/{1}", Path.GetDirectoryName(location).Replace('\\', '/'), directoryName);
            objectFolder = objectFolder.EndsWith("/") ? objectFolder.Substring(0, objectFolder.Length - 1) : objectFolder;
            string[] objectGuids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new string[] { objectFolder });

            SerializedObject serializedObject = new SerializedObject(level);
            serializedObject.Update();

            SerializedProperty objectsProperty = serializedObject.FindProperty(propertyName);
            bool dirty = false;

            if (objectsProperty.arraySize != objectGuids.Length)
            {
                objectsProperty.arraySize = objectGuids.Length;
            }

            for (int i = 0; i < objectGuids.Length; ++i)
            {
                string objectPath = AssetDatabase.GUIDToAssetPath(objectGuids[i]);
                T obj = AssetDatabase.LoadAssetAtPath<T>(objectPath);

                SerializedProperty arrayElement = objectsProperty.GetArrayElementAtIndex(i);

                if (obj != arrayElement.objectReferenceValue)
                {
                    arrayElement.objectReferenceValue = obj;
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

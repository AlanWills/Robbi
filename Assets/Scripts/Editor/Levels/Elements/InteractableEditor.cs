using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(Interactable))]
    public class InteractableEditor : Editor
    {
        #region Properties and Fields

        public Interactable Switch
        {
            get { return target as Interactable; }
        }

        private SerializedProperty interactedModifiersProperty;

        private int selectedModifierType = 0;

        private static Type[] modifierTypes = new Type[]
        {
            typeof(OpenDoorModifier),
            typeof(CloseDoorModifier),
            typeof(ToggleDoorModifier),
        };

        private static string[] modifierDisplayNames = new string[]
        {
            "Open Door",
            "Close Door",
            "Toggle Door",
        };

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            interactedModifiersProperty = serializedObject.FindProperty("interactedModifiers");
        }

        #endregion


        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "interatedModifiers");

            EditorGUILayout.Space();
            RobbiEditorGUILayout.HorizontalLine();
           
            for (int i = interactedModifiersProperty.arraySize; i > 0; --i)
            {
                Editor modifierEditor = Editor.CreateEditor(interactedModifiersProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue);
                modifierEditor.OnInspectorGUI();

                EditorGUILayout.Space();

                if (GUILayout.Button("Remove"))
                {
                    Switch.RemoveInteractedModifier(i - 1);
                }

                RobbiEditorGUILayout.HorizontalLine();
            }

            selectedModifierType = EditorGUILayout.Popup(selectedModifierType, modifierDisplayNames);

            EditorGUILayout.Space();

            if (GUILayout.Button("Add Modifier"))
            {
                Switch.AddInteractedModifier(modifierTypes[selectedModifierType]);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

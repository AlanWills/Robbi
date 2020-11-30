using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using RobbiEditor.Utils;
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

        private Interactable Interactable
        {
            get { return target as Interactable; }
        }

        private SerializedProperty interactedModifiersProperty;
        private int selectedModifierType = 0;
        private bool isMainAsset;

        private static Type[] modifierTypes = new Type[]
        {
            typeof(RaiseDoorEvent),
        };

        private static string[] modifierDisplayNames = new string[]
        {
            "Raise Door Event",
        };

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            interactedModifiersProperty = serializedObject.FindProperty("interactedModifiers");
            isMainAsset = AssetDatabase.IsMainAsset(target);
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "interactedModifiers");

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Modifiers", RobbiEditorStyles.BoldLabel);

            if (isMainAsset && GUILayout.Button("Apply Hide Flags", GUILayout.ExpandWidth(false)))
            {
                AssetUtility.ApplyHideFlags(Interactable, HideFlags.HideInHierarchy);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            {
                ++EditorGUI.indentLevel;

                for (int i = interactedModifiersProperty.arraySize; i > 0; --i)
                {
                    LevelModifier levelModifier = interactedModifiersProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue as LevelModifier;

                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(levelModifier.name, RobbiEditorStyles.BoldLabel);

                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            Interactable.RemoveInteractedModifier(i - 1);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    Editor modifierEditor = Editor.CreateEditor(levelModifier);
                    modifierEditor.OnInspectorGUI();

                    EditorGUILayout.Space();

                    RobbiEditorGUILayout.HorizontalLine();
                }

                --EditorGUI.indentLevel;
            }

            EditorGUILayout.BeginHorizontal();
            {
                selectedModifierType = EditorGUILayout.Popup(selectedModifierType, modifierDisplayNames);

                EditorGUILayout.Space();

                if (GUILayout.Button("Add Modifier", GUILayout.ExpandWidth(false)))
                {
                    Interactable.AddInteractedModifier(modifierTypes[selectedModifierType]);
                }
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

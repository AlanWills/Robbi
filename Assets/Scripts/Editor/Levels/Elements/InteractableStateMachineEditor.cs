using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using RobbiEditor.Popups;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(InteractableStateMachine))]
    public class InteractableStateMachineEditor : Editor
    {
        #region Properties and Fields

        private InteractableStateMachine InteractableStateMachine
        {
            get { return target as InteractableStateMachine; }
        }

        private SerializedProperty statesProperty;
        private bool isMainAsset;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            statesProperty = serializedObject.FindProperty("states");
            isMainAsset = AssetDatabase.IsMainAsset(target);
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "states");

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("States", RobbiEditorStyles.BoldLabel);
            {
                ++EditorGUI.indentLevel;

                for (int i = statesProperty.arraySize; i > 0; --i)
                {
                    Interactable state = statesProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue as Interactable;
                    
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(string.Format("{0} (State Index {1})", state.name, i - 1), RobbiEditorStyles.BoldLabel);

                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            InteractableStateMachine.RemoveState(i - 1);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    {
                        ++EditorGUI.indentLevel;

                        Editor interactableEditor = Editor.CreateEditor(state);
                        interactableEditor.OnInspectorGUI();

                        --EditorGUI.indentLevel;
                    }
                    EditorGUILayout.Space();

                    RobbiEditorGUILayout.HorizontalLine();
                }

                if (GUILayout.Button("Add State", GUILayout.ExpandWidth(false)))
                {
                    TextInputPopup.Display("New State...", (string stateName) =>
                    {
                        InteractableStateMachine.AddState(stateName);
                    });
                }

                --EditorGUI.indentLevel;
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

using CelesteEditor;
using CelesteEditor.Popups;
using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using RobbiEditor.Constants;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        private InteractableStateMachine copyFrom;
        private List<Editor> interactableEditors = new List<Editor>();

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            statesProperty = serializedObject.FindProperty("states");
            
            interactableEditors.Clear();
            for (int i = 0; i < InteractableStateMachine.NumStates; ++i)
            {
                interactableEditors.Add(Editor.CreateEditor(InteractableStateMachine.GetState(i)));
            }
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "states");

            if (GUILayout.Button("Create Toggle Switch"))
            {
                InteractableStateMachine stateMachine = InteractableStateMachine;

                {
                    Interactable toggleLeft = stateMachine.AddState("Toggle Left");
                    toggleLeft.InteractedTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.TOGGLE_LEFT_TILE);
                    ToggleDoor toggleDoorLeft = toggleLeft.AddInteractedModifier<ToggleDoor>();
                    toggleDoorLeft.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                }

                {
                    Interactable toggleRight = stateMachine.AddState("Toggle Right");
                    toggleRight.InteractedTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.TOGGLE_RIGHT_TILE);
                    ToggleDoor toggleDoorRight = toggleRight.AddInteractedModifier<ToggleDoor>();
                    toggleDoorRight.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                }
            }

            EditorGUILayout.BeginHorizontal();
            copyFrom = EditorGUILayout.ObjectField(copyFrom, typeof(InteractableStateMachine), false) as InteractableStateMachine;

            if (GUILayout.Button("Copy", GUILayout.ExpandWidth(false)))
            {
                CopyFrom();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("States", CelesteEditorStyles.BoldLabel);
            {
                ++EditorGUI.indentLevel;

                for (int i = statesProperty.arraySize; i > 0; --i)
                {
                    Interactable state = statesProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue as Interactable;
                    
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(string.Format("{0} (State Index {1})", state.name, i - 1), CelesteEditorStyles.BoldLabel);

                        if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                        {
                            InteractableStateMachine.RemoveState(i - 1);
                            interactableEditors.RemoveAt(i - 1);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();
                    {
                        ++EditorGUI.indentLevel;

                        interactableEditors[i - 1].OnInspectorGUI();

                        --EditorGUI.indentLevel;
                    }
                    EditorGUILayout.Space();

                    CelesteEditorGUILayout.HorizontalLine();
                }

                if (GUILayout.Button("Add State", GUILayout.ExpandWidth(false)))
                {
                    TextInputPopup.Display("New State...", (string stateName) =>
                    {
                        interactableEditors.Add(Editor.CreateEditor(InteractableStateMachine.AddState(stateName)));
                    });
                }

                --EditorGUI.indentLevel;
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion

        #region Utility

        private void CopyFrom()
        {
            InteractableStateMachine copyTo = InteractableStateMachine;
            for (int i = 0; i < copyFrom.NumStates; ++i)
            {
                Interactable originalState = copyFrom.GetState(i);
                Interactable copyState = copyTo.AddState(originalState.name);
                CopyState(originalState, copyState);
            }

            EditorUtility.SetDirty(copyTo);
        }

        private void CopyState(Interactable original, Interactable copyTo)
        {
            copyTo.InteractedTile = original.InteractedTile;
            copyTo.UninteractedTile = original.UninteractedTile;

            for (int i = 0; i < original.NumInteractedModifiers; ++i)
            {
                LevelModifier levelModifier = original.GetInteractedModifier(i);
                LevelModifier copyModifier = copyTo.AddInteractedModifier(levelModifier.GetType());
                copyModifier.CopyFrom(levelModifier);

                EditorUtility.SetDirty(copyModifier);
            }
        }

        #endregion
    }
}

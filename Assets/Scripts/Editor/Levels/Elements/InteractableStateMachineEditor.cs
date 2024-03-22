using Celeste;
using CelesteEditor;
using CelesteEditor.Popups;
using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using RobbiEditor.Constants;
using System;
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
                AddStateEditor(InteractableStateMachine.GetState(i));
            }
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "states");

            EditorGUI.BeginChangeCheck();
            int selectedColour = EditorGUILayout.Popup("Create Toggle", 0, DoorColours.COLOUR_NAMES);
            if (EditorGUI.EndChangeCheck())
            {
                Tuple<string, string> tileFiles = TileFiles.TOGGLE_TILES[selectedColour];

                {
                    Interactable toggleUp = InteractableStateMachine.AddState("Toggle Up");
                    toggleUp.InteractedTile = AssetDatabase.LoadAssetAtPath<Tile>(tileFiles.Item1);
                    ToggleDoor toggleDoorLeft = toggleUp.AddInteractedModifier<ToggleDoor>();
                    toggleDoorLeft.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                    
                    AddStateEditor(toggleUp);
                }

                {
                    Interactable toggleDown = InteractableStateMachine.AddState("Toggle Down");
                    toggleDown.InteractedTile = AssetDatabase.LoadAssetAtPath<Tile>(tileFiles.Item2);
                    ToggleDoor toggleDoorRight = toggleDown.AddInteractedModifier<ToggleDoor>();
                    toggleDoorRight.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);

                    AddStateEditor(toggleDown);
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
            EditorGUILayout.LabelField("States", CelesteGUIStyles.BoldLabel);
            {
                ++EditorGUI.indentLevel;

                for (int i = statesProperty.arraySize; i > 0; --i)
                {
                    Interactable state = statesProperty.GetArrayElementAtIndex(i - 1).objectReferenceValue as Interactable;
                    
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.LabelField(string.Format("{0} (State Index {1})", state.name, i - 1), CelesteGUIStyles.BoldLabel);

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
                        AddStateEditor(InteractableStateMachine.AddState(stateName));
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
                AddStateEditor(copyState);
            }

            EditorUtility.SetDirty(copyTo);
        }

        private void CopyState(Interactable original, Interactable copyTo)
        {
            copyTo.InteractedTile = original.InteractedTile;

            for (int i = 0; i < original.NumInteractedModifiers; ++i)
            {
                LevelModifier levelModifier = original.GetInteractedModifier(i);
                LevelModifier copyModifier = copyTo.AddInteractedModifier(levelModifier.GetType());
                copyModifier.CopyFrom(levelModifier);

                EditorUtility.SetDirty(copyModifier);
            }

        }

        private void AddStateEditor(Interactable state)
        {
            interactableEditors.Add(Editor.CreateEditor(state));
        }

        #endregion
    }
}

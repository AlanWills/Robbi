﻿using Robbi.Events.Levels.Elements;
using Robbi.Levels.Elements;
using Robbi.Levels.Modifiers;
using RobbiEditor.Constants;
using RobbiEditor.Popups;
using RobbiEditor.Utils;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            statesProperty = serializedObject.FindProperty("states");
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
                    RaiseDoorEvent toggleDoorLeft = toggleLeft.AddInteractedModifier<RaiseDoorEvent>();
                    toggleDoorLeft.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                }

                {
                    Interactable toggleRight = stateMachine.AddState("Toggle Right");
                    toggleRight.InteractedTile = AssetDatabase.LoadAssetAtPath<Tile>(TileFiles.TOGGLE_RIGHT_TILE);
                    RaiseDoorEvent toggleDoorRight = toggleRight.AddInteractedModifier<RaiseDoorEvent>();
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
                RaiseDoorEvent raiseDoorEvent = original.GetInteractedModifier(i) as RaiseDoorEvent;
                RaiseDoorEvent copyModifier = copyTo.AddInteractedModifier(raiseDoorEvent.GetType()) as RaiseDoorEvent;
                copyModifier.door = raiseDoorEvent.door;
                copyModifier.doorEvent = raiseDoorEvent.doorEvent;

                EditorUtility.SetDirty(copyModifier);
            }
        }

        #endregion
    }
}

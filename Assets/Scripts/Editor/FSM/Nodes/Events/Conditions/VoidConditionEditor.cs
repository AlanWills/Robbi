﻿using Robbi.FSM.Nodes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Events.Conditions
{
    public class VoidEventConditionEditor : EventConditionEditor
    {
        protected override void OnGUI(MultiEventListenerNode listenerNode, SerializedObject eventConditionObject)
        {
            SerializedProperty nameProperty = eventConditionObject.FindProperty("m_Name");
            SerializedProperty listenForProperty = eventConditionObject.FindProperty("listenFor");
            UnityEngine.Object oldValue = listenForProperty.objectReferenceValue;

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(listenForProperty);

            if (EditorGUI.EndChangeCheck())
            {
                UnityEngine.Object newValue = listenForProperty.objectReferenceValue;

                if (oldValue == null && newValue != null)
                {
                    nameProperty.stringValue = newValue.name;
                    listenerNode.AddEventConditionPort(newValue.name);
                }
                else if (oldValue != null && newValue != null && oldValue != newValue)
                {
                    nameProperty.stringValue = newValue.name;
                    listenerNode.RemoveDynamicPort(oldValue.name);
                    listenerNode.AddEventConditionPort(newValue.name);
                }
                else if (oldValue != null && newValue == null)
                {
                    nameProperty.stringValue = "";
                    listenerNode.RemoveDynamicPort(oldValue.name);
                }
            }
        }
    }
}

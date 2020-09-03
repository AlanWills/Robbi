using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes.Events
{
    public abstract class EventConditionEditor
    {
        public void GUI(MultiEventListenerNode listenerNode, EventCondition eventCondition)
        {
            SerializedObject serializedObject = new SerializedObject(eventCondition);
            serializedObject.Update();

            OnGUI(listenerNode, serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void OnGUI(MultiEventListenerNode listenerNode, SerializedObject eventConditionObject);
    }

    public class VoidEventConditionEditor : EventConditionEditor
    {
        protected override void OnGUI(MultiEventListenerNode listenerNode, SerializedObject eventConditionObject)
        {
            SerializedProperty listenForProperty = eventConditionObject.FindProperty("listenFor");
            
            EditorGUI.BeginChangeCheck();

            UnityEngine.Object oldValue = listenForProperty.objectReferenceValue;
            EditorGUILayout.PropertyField(listenForProperty);
            
            if (EditorGUI.EndChangeCheck())
            {
                UnityEngine.Object newValue = listenForProperty.objectReferenceValue;

                if (oldValue == null && newValue != null)
                {
                    listenerNode.AddEventConditionPort(newValue.name);
                }

                // Handle other cases, e.g. set to null and switching from non-null
            }
        }
    }

    [CustomNodeEditor(typeof(MultiEventListenerNode))]
    public class MultiEventListenerNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.8f, 0.9f, 0); }
        }

        private static Type[] eventOptions = new Type[]
        {
            typeof(VoidEventCondition)
        };

        private static Dictionary<Type, EventConditionEditor> eventConditionEditorFactory = new Dictionary<Type, EventConditionEditor>()
        {
            { typeof(VoidEventCondition), new VoidEventConditionEditor() },
        };

        private int selectedEventType = 0;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            MultiEventListenerNode eventListenerNode = target as MultiEventListenerNode;

            NodeEditorGUILayout.PortField(eventListenerNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME));

            if (EditorGUILayout.DropdownButton(new GUIContent("Add Event"), FocusType.Keyboard))
            {
                string[] options = eventOptions.Select(x => x.Name).ToArray();
                selectedEventType = EditorGUILayout.Popup(selectedEventType, options);

                eventListenerNode.AddEvent(eventOptions[selectedEventType]);
            }

            foreach (EventCondition eventCondition in eventListenerNode.Events)
            {
                EditorGUILayout.BeginHorizontal();

                if (eventConditionEditorFactory.TryGetValue(eventCondition.GetType(), out EventConditionEditor editor))
                {
                    editor.GUI(eventListenerNode, eventCondition);
                }

                EditorGUILayout.EndHorizontal();
            }

            Really want this to be on the same line as the event condition stuff
            Specify rects maybe?
            foreach (var port in eventListenerNode.DynamicOutputs)
            {
                if (port.fieldName != FSMNode.DEFAULT_OUTPUT_PORT_NAME)
                {
                    NodeEditorGUILayout.PortField(port);
                }
            }

            //base.OnBodyGUI();
        }

        #endregion
    }
}

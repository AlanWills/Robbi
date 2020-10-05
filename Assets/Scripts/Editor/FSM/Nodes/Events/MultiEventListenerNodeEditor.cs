using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using RobbiEditor.FSM.Nodes.Events.Conditions;
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
    [CustomNodeEditor(typeof(MultiEventListenerNode))]
    public class MultiEventListenerNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        private static Type[] eventOptions = new Type[]
        {
            typeof(VoidEventCondition),
            typeof(Vector3IntEventCondition),
        };

        private static string[] eventDisplayNames = new string[]
        {
            "Void",
            "Vector3Int",
        };

        private static Dictionary<Type, EventConditionEditor> eventConditionEditorFactory = new Dictionary<Type, EventConditionEditor>()
        {
            { typeof(VoidEventCondition), new EventConditionEditor() },
            { typeof(Vector3IntEventCondition), new EventConditionEditor() },
        };

        private int selectedEventType = 0;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            MultiEventListenerNode eventListenerNode = target as MultiEventListenerNode;

            NodeEditorGUILayout.PortPair(
                eventListenerNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME),
                eventListenerNode.GetOutputPort(nameof(eventListenerNode.argument)));

            selectedEventType = EditorGUILayout.Popup(selectedEventType, eventDisplayNames);

            if (GUILayout.Button("Add Condition"))
            {
                eventListenerNode.AddEvent(eventOptions[selectedEventType]);
            }

            for (uint i = eventListenerNode.NumEvents; i > 0; --i)
            {
                EditorGUILayout.BeginHorizontal();

                EventCondition eventCondition = eventListenerNode.GetEvent(i - 1);

                if (GUILayout.Button("-", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    eventListenerNode.RemoveEvent(eventCondition);
                }
                else if (i > 0 &&
                         GUILayout.Button("v", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    eventListenerNode.SwapEvents(i - 1, i - 2);
                }
                else if (i < eventListenerNode.NumEvents &&
                         GUILayout.Button("^", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    eventListenerNode.SwapEvents(i - 1, i);
                }

                if (eventConditionEditorFactory.TryGetValue(eventCondition.GetType(), out EventConditionEditor editor))
                {
                    editor.GUI(eventListenerNode, eventCondition);
                }

                Rect rect = GUILayoutUtility.GetLastRect();
                NodeEditorGUILayout.PortField(rect.position + new Vector2(rect.width, 0), eventListenerNode.GetOutputPort(eventCondition.name));

                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion
    }
}
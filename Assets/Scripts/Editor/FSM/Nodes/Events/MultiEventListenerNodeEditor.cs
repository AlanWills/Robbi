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

            for (uint i = eventListenerNode.NumEvents; i > 0; --i)
            {
                EditorGUILayout.BeginHorizontal();

                EventCondition eventCondition = eventListenerNode.GetEvent(i - 1);

                if (GUILayout.Button("-", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    eventListenerNode.RemoveEvent(eventCondition);
                }
                else
                {
                    if (eventConditionEditorFactory.TryGetValue(eventCondition.GetType(), out EventConditionEditor editor))
                    {
                        editor.GUI(eventListenerNode, eventCondition);
                    }

                    Rect rect = GUILayoutUtility.GetLastRect();
                    NodeEditorGUILayout.PortField(rect.position + new Vector2(rect.width, 0), eventListenerNode.GetOutputPort(eventCondition.name));
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion
    }
}
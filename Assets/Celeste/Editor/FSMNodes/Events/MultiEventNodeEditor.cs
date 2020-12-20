﻿using Celeste.FSM;
using Celeste.FSM.Nodes;
using Celeste.FSM.Nodes.Events;
using Celeste.FSM.Nodes.Events.Conditions;
using RobbiEditor.FSM.Nodes.Events.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes.Events
{
    [CustomNodeEditor(typeof(MultiEventNode))]
    public class MultiEventNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        private int selectedEventType = 0;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            MultiEventNode multiEventNode = target as MultiEventNode;

            NodeEditorGUILayout.PortPair(
                multiEventNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME),
                multiEventNode.GetOutputPort(nameof(multiEventNode.argument)));

            selectedEventType = EditorGUILayout.Popup(selectedEventType, EventConditionEditor.EventDisplayNames);

            if (GUILayout.Button("Add Condition"))
            {
                multiEventNode.AddEvent(EventConditionEditor.EventOptions[selectedEventType]);
            }

            for (uint i = multiEventNode.NumEvents; i > 0; --i)
            {
                EditorGUILayout.BeginHorizontal();

                EventCondition eventCondition = multiEventNode.GetEvent(i - 1);

                if (GUILayout.Button("-", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    multiEventNode.RemoveEvent(eventCondition);
                }
                else if (i > 0 &&
                         GUILayout.Button("v", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    multiEventNode.SwapEvents(i - 1, i - 2);
                }
                else if (i < multiEventNode.NumEvents &&
                         GUILayout.Button("^", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16)))
                {
                    multiEventNode.SwapEvents(i - 1, i);
                }

                EventConditionEditor.DefaultEventConditionEditor.GUI(multiEventNode, eventCondition);

                Rect rect = GUILayoutUtility.GetLastRect();
                NodeEditorGUILayout.PortField(rect.position + new Vector2(rect.width, 0), multiEventNode.GetOutputPort(eventCondition.name));

                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion
    }
}

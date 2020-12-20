﻿using Celeste.FSM;
using Celeste.FSM.Nodes;
using Celeste.FSM.Nodes.Logic;
using Celeste.FSM.Nodes.Logic.Conditions;
using CelesteEditor;
using CelesteEditor.Popups;
using RobbiEditor.FSM.Nodes.Logic.Conditions;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes.Logic
{
    [CustomNodeEditor(typeof(IfNode))]
    public class IfNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        private static Type[] valueConditionOptions = new Type[]
        {
            typeof(BoolValueCondition),
            typeof(IntValueCondition),
            typeof(UIntValueCondition),
            typeof(FloatValueCondition),
            typeof(LongValueCondition),
            typeof(StringValueCondition),
            typeof(Vector3IntValueCondition),
            typeof(InVector3IntArrayCondition)
        };

        private static string[] valueConditionDisplayNames = new string[]
        {
            "Bool",
            "Int",
            "UInt",
            "Float",
            "Long",
            "String",
            "Vector3Int",
            "In Vector3IntArray"
        };

        private static Dictionary<Type, ValueConditionEditor> valueConditionEditorFactory = new Dictionary<Type, ValueConditionEditor>()
        {
            { typeof(BoolValueCondition), new BoolValueConditionEditor() },
            { typeof(IntValueCondition), new IntValueConditionEditor() },
            { typeof(UIntValueCondition), new UIntValueConditionEditor() },
            { typeof(FloatValueCondition), new FloatValueConditionEditor() },
            { typeof(LongValueCondition), new LongValueConditionEditor() },
            { typeof(StringValueCondition), new StringValueConditionEditor() },
            { typeof(Vector3IntValueCondition), new Vector3IntValueConditionEditor() },
            { typeof(InVector3IntArrayCondition), new InVector3IntArrayConditionEditor() }
        };

        private int selectedEventType = 0;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            using (new LabelColourResetter(Color.black))
            {
                IfNode ifNode = target as IfNode;

                NodeEditorGUILayout.PortPair(
                    ifNode.GetInputPort(FSMNode.DEFAULT_INPUT_PORT_NAME),
                    ifNode.GetOutputPort(FSMNode.DEFAULT_OUTPUT_PORT_NAME));

                NodeEditorGUILayout.PortPair(
                    ifNode.GetInputPort(nameof(ifNode.inArgument)),
                    ifNode.GetOutputPort(nameof(ifNode.outArgument)));

                selectedEventType = EditorGUILayout.Popup(selectedEventType, valueConditionDisplayNames);

                if (GUILayout.Button("Add Condition"))
                {
                    TextInputPopup.Display("New Condition...", (string conditionName) =>
                    {
                        ifNode.AddCondition(conditionName, valueConditionOptions[selectedEventType]);
                    });
                }

                for (uint i = ifNode.NumConditions; i > 0; --i)
                {
                    ValueCondition valueCondition = ifNode.GetCondition(i - 1);

                    EditorGUILayout.Separator();
                    EditorGUILayout.BeginHorizontal();

                    bool removeCondition = GUILayout.Button("-", GUILayout.MaxWidth(16), GUILayout.MaxHeight(16));
                    EditorGUILayout.LabelField(valueCondition.name);
                    Rect rect = GUILayoutUtility.GetLastRect();
                    NodeEditorGUILayout.PortField(rect.position + new Vector2(rect.width, 0), ifNode.GetOutputPort(valueCondition.name));

                    EditorGUILayout.EndHorizontal();

                    if (removeCondition)
                    {
                        ifNode.RemoveCondition(valueCondition);
                    }
                    else if (valueConditionEditorFactory.TryGetValue(valueCondition.GetType(), out ValueConditionEditor editor))
                    {
                        editor.GUI(ifNode, valueCondition);
                    }
                }
            }
        }

        #endregion
    }
}
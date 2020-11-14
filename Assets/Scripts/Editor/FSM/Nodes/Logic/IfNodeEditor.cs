using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using RobbiEditor.FSM.Nodes.Logic.Conditions;
using RobbiEditor.Popups;
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
            typeof(LongValueCondition),
            typeof(Vector3IntValueCondition),
            typeof(InVector3IntArrayCondition),
            typeof(InTilemapCondition),
            typeof(AtSwitchCondition),
        };

        private static string[] valueConditionDisplayNames = new string[]
        {
            "Bool",
            "Int",
            "UInt",
            "Long",
            "Vector3Int",
            "In Vector3IntArray",
            "In Tilemap",
            "At Switch",
        };

        private static Dictionary<Type, ValueConditionEditor> valueConditionEditorFactory = new Dictionary<Type, ValueConditionEditor>()
        {
            { typeof(BoolValueCondition), new BoolValueConditionEditor() },
            { typeof(IntValueCondition), new IntValueConditionEditor() },
            { typeof(UIntValueCondition), new UIntValueConditionEditor() },
            { typeof(LongValueCondition), new LongValueConditionEditor() },
            { typeof(Vector3IntValueCondition), new Vector3IntValueConditionEditor() },
            { typeof(InVector3IntArrayCondition), new InVector3IntArrayConditionEditor() },
            { typeof(InTilemapCondition), new InTilemapConditionEditor() },
            { typeof(AtSwitchCondition), new AtSwitchConditionEditor() },
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
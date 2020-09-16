using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using RobbiEditor.FSM.Nodes.Events.Conditions;
using RobbiEditor.FSM.Nodes.Logic.Conditions;
using RobbiEditor.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNode;
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
            typeof(Vector3IntValueCondition),
        };

        private static string[] valueConditionDisplayNames = new string[]
        {
            "Bool",
            "Int",
            "Vector3Int",
        };

        private static Dictionary<Type, ValueConditionEditor> valueConditionEditorFactory = new Dictionary<Type, ValueConditionEditor>()
        {
            { typeof(BoolValueCondition), new BoolValueConditionEditor() },
            { typeof(IntValueCondition), new IntValueConditionEditor() },
            { typeof(Vector3IntValueCondition), new Vector3IntValueConditionEditor() },
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
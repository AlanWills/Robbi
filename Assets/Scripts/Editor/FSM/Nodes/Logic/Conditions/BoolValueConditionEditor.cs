using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using Robbi.Logic;
using RobbiEditor.PropertyDrawers.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public class BoolValueConditionEditor : ValueConditionEditor
    {
        protected override void OnGUI(IfNode ifNode, SerializedObject valueCondition)
        {
            EditorGUILayout.BeginVertical();

            SerializedProperty valueProperty = valueCondition.FindProperty("value");
            SerializedProperty conditionProperty = valueCondition.FindProperty("condition");

            EditorGUILayout.PropertyField(valueProperty, GUIContent.none);

            string[] operatorDisplayNames = new string[]
            {
                "Equals",
                "Not Equals"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals
            };

            int chosenOperator = conditionProperty.enumValueIndex;
            chosenOperator = EditorGUILayout.IntPopup(chosenOperator, operatorDisplayNames, operators);
            conditionProperty.enumValueIndex = chosenOperator;

            ParameterReferencePropertyDrawer.OnGUI((valueCondition.targetObject as BoolValueCondition).target);

            EditorGUILayout.EndVertical();
        }
    }
}

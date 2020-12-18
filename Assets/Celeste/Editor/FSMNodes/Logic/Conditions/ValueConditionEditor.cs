using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using RobbiEditor.PropertyDrawers.Parameters;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public abstract class ValueConditionEditor
    {
        public void GUI(IfNode ifNode, ValueCondition valueCondition)
        {
            SerializedObject serializedObject = new SerializedObject(valueCondition);
            serializedObject.Update();

            OnGUI(ifNode, serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void OnGUI(IfNode ifNode, SerializedObject eventConditionObject);

        protected void DrawDefaultGUI(SerializedObject valueCondition, string[] operatorDisplayNames, int[] operators, Object conditionTarget)
        {
            EditorGUILayout.BeginVertical();

            SerializedProperty valueProperty = valueCondition.FindProperty("value");
            SerializedProperty conditionProperty = valueCondition.FindProperty("condition");
            SerializedProperty useArgumentProperty = valueCondition.FindProperty("useArgument");

            EditorGUILayout.PropertyField(valueProperty, GUIContent.none);

            int chosenOperator = conditionProperty.enumValueIndex;
            chosenOperator = EditorGUILayout.IntPopup(chosenOperator, operatorDisplayNames, operators);
            conditionProperty.enumValueIndex = chosenOperator;

            EditorGUILayout.PropertyField(useArgumentProperty);

            if (!useArgumentProperty.boolValue)
            {
                ParameterReferencePropertyDrawer.OnGUI(conditionTarget);
            }

            EditorGUILayout.EndVertical();
        }
    }
}

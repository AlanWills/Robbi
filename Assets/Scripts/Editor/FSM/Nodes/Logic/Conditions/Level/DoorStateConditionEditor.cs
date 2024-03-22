using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using UnityEditor;
using CelesteEditor.Logic;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(DoorStateCondition))]
    public class DoorStateConditionEditor : ConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
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

            EditorGUILayout.BeginVertical();

            EditorGUILayout.PropertyField(valueCondition.FindProperty(nameof(DoorStateCondition.door)));
            EditorGUILayout.PropertyField(valueCondition.FindProperty(nameof(DoorStateCondition.doorsTilemap)));

            SerializedProperty conditionProperty = valueCondition.FindProperty("condition");
            int chosenOperator = conditionProperty.enumValueIndex;
            chosenOperator = EditorGUILayout.IntPopup(chosenOperator, operatorDisplayNames, operators);
            conditionProperty.enumValueIndex = chosenOperator;

            EditorGUILayout.PropertyField(valueCondition.FindProperty(nameof(DoorStateCondition.doorState)));
            
            EditorGUILayout.EndVertical();
        }
    }
}

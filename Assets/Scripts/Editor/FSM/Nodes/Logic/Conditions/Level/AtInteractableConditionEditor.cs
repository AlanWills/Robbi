using Robbi.FSM.Nodes.Logic.Conditions;
using UnityEditor;
using Celeste.Logic;
using CelesteEditor.Logic;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(AtInteractableCondition))]
    public class AtInteractableConditionEditor : ParameterizedValueConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "At Interactable",
                "Not At Interactable"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals
            };

            DrawDefaultInspectorGUI(valueCondition, operatorDisplayNames, operators);
        }
    }
}

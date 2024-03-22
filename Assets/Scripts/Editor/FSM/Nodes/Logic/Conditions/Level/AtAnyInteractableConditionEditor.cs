using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using UnityEditor;
using CelesteEditor.Logic;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(AtAnyInteractableCondition))]
    public class AtAnyInteractableConditionEditor : ParameterizedValueConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "At Any Interactable",
                "At No Interactables"
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

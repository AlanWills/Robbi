using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Parameters;
using UnityEditor;
using Celeste.FSM.Nodes.Logic;
using Celeste.Logic;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public class AtInteractableConditionEditor : ValueConditionEditor
    {
        protected override void OnGUI(IfNode ifNode, SerializedObject valueCondition)
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

            Vector3IntReference reference = (valueCondition.targetObject as AtInteractableCondition).target;
            DrawDefaultGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}

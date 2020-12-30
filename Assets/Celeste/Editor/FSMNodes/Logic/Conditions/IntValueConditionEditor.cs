using Celeste.FSM.Nodes.Logic;
using Celeste.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using Celeste.Parameters;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public class IntValueConditionEditor : ValueConditionEditor
    {
        protected override void OnGUI(IfNode ifNode, SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "Equals",
                "Not Equals",
                "Less Than",
                "Less Than Or Equal To",
                "Greater Than",
                "Greater Than Or Equal To"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals,
                (int)ConditionOperator.LessThan,
                (int)ConditionOperator.LessThanOrEqualTo,
                (int)ConditionOperator.GreaterThan,
                (int)ConditionOperator.GreaterThanOrEqualTo,
            };

            IntReference reference = (valueCondition.targetObject as IntValueCondition).target;
            DrawDefaultGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}

using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Parameters;
using UnityEditor;
using Celeste.FSM.Nodes.Logic;
using Celeste.Logic;
using CelesteEditor.FSM.Nodes.Logic.Conditions;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(AtInteractableCondition))]
    public class AtInteractableConditionEditor : ConditionEditor
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

            Vector3IntReference reference = (valueCondition.targetObject as AtInteractableCondition).target;
            DrawDefaultInspectorGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}

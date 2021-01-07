using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Parameters;
using UnityEditor;
using Celeste.FSM.Nodes.Logic;
using Celeste.Logic;
using CelesteEditor.FSM.Nodes.Logic.Conditions;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(AtCollectableCondition))]
    public class AtCollectableConditionEditor : ConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "At Collectable",
                "Not At Collectable"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals
            };

            Vector3IntReference reference = (valueCondition.targetObject as AtCollectableCondition).target;
            DrawDefaultInspectorGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}

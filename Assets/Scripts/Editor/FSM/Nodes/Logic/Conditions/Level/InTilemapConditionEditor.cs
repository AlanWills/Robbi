using Robbi.FSM.Nodes.Logic.Conditions;
using UnityEditor;
using Celeste.Logic;
using CelesteEditor.Logic;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(InTilemapCondition))]
    public class InTilemapConditionEditor : ParameterizedValueConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "Has Tile",
                "Does Not Have Tile"
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

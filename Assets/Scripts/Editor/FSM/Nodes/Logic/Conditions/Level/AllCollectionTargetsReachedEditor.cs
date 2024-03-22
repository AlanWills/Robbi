using CelesteEditor.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    [CustomEditor(typeof(AllCollectionTargetsReached))]
    public class AllCollectionTargetsReachedEditor : ConditionEditor
    {
        protected override void OnInspectorGUIImpl(SerializedObject valueCondition)
        {
            DrawPropertiesExcluding(valueCondition, "m_Script");
        }
    }
}

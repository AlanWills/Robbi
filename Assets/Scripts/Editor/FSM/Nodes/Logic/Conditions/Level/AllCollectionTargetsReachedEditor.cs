using Robbi.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using Celeste.FSM.Nodes.Logic;
using CelesteEditor.FSM.Nodes.Logic.Conditions;

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

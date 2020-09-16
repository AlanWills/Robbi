using Robbi.FSM.Nodes.Logic.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Logic
{
    [Serializable]
    [CreateNodeMenu("Robbi/Logic/If")]
    [NodeTint(0.0f, 1, 1)]
    public class IfNode : FSMNode
    {
        #region Properties and Fields

        public uint NumConditions
        {
            get { return (uint)conditions.Count; }
        }

        [SerializeField]
        private List<ValueCondition> conditions = new List<ValueCondition>();

        #endregion

        #region Add/Remove/Copy

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            IfNode originalIfNode = original as IfNode;

            for (uint i = 0; i < originalIfNode.NumConditions; ++i)
            {
                ValueCondition originalCondition = originalIfNode.GetCondition(i);
                ValueCondition newCondition = AddCondition(originalCondition.name, originalCondition.GetType());
                newCondition.CopyFrom(originalCondition);
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            for (uint i = NumConditions; i > 0; --i)
            {
                RemoveCondition(GetCondition(i));
            }
        }

        #endregion

        #region Condition Methods

        public ValueCondition AddCondition(string conditionName, Type conditionType)
        {
            ValueCondition valueCondition = ScriptableObject.CreateInstance(conditionType) as ValueCondition;
            valueCondition.name = conditionName;
            conditions.Add(valueCondition);

#if UNITY_EDITOR
            valueCondition.Init_EditorOnly(graph as FSMGraph);

            AssetUtils.EditorOnly.AddObjectToAsset(valueCondition, graph);
            AssetUtils.EditorOnly.SaveAndRefresh();
#endif

            AddOutputPort(conditionName);

            return valueCondition;
        }

        public T AddCondition<T>(string conditionName) where T : ValueCondition
        {
            return AddCondition(conditionName, typeof(T)) as T;
        }

        public void RemoveCondition(ValueCondition condition)
        {
            RemoveDynamicPort(condition.name);
            conditions.Remove(condition);

#if UNITY_EDITOR
            condition.Cleanup_EditorOnly(graph as FSMGraph);

            AssetUtils.EditorOnly.RemoveObjectFromAsset(condition);
            AssetUtils.EditorOnly.SaveAndRefresh();
#endif
        }

        public ValueCondition GetCondition(uint conditionIndex)
        {
            return conditionIndex < NumConditions ? conditions[(int)conditionIndex] : null;
        }

        #endregion

        #region FSM Runtime

        protected override FSMNode OnUpdate()
        {
            foreach (ValueCondition condition in conditions)
            {
                if (condition.Check())
                {
                    return GetConnectedNode(condition.name);
                }
            }

            // Fall back to else port
            return base.OnUpdate();
        }

        #endregion
    }
}

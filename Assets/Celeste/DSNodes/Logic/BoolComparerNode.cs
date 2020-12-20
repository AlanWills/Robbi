using Robbi.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;
using static XNode.Node;

namespace Robbi.DataSystem.Nodes.Logic
{
    [Serializable]
    [CreateNodeMenu("Robbi/Logic/Bool Comparer")]
    [NodeTint(0.0f, 0.75f, 0.75f)]
    public class BoolComparerNode : DataNode
    {
        #region Properties and Fields

        [Input]
        public bool lhs;

        public ConditionOperator condition;

        [Input]
        public bool rhs;

        [Output]
        public bool result;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            bool lhsValue = GetInputValue("lhs", lhs);
            bool rhsValue = GetInputValue("rhs", rhs);

            switch (condition)
            {
                case ConditionOperator.Equals:
                    return lhsValue == rhsValue;

                case ConditionOperator.NotEquals:
                    return lhsValue != rhsValue;

                default:
                    Debug.LogErrorFormat("Unhandled case {0} in BoolComparerNode", condition);
                    return false;
            }
        }

        #endregion
    }
}

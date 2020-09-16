﻿using Robbi.Logic;
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
    [CreateNodeMenu("Robbi/Logic/Int Comparer")]
    [NodeTint(0.0f, 0.75f, 0.75f)]
    public class IntComparerNode : DataNode
    {
        #region Properties and Fields

        [Input]
        public int lhs;

        public ConditionOperator condition;

        [Input]
        public int rhs;

        [Output]
        public bool result;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            int lhsValue = GetInputValue<int>("lhs");
            int rhsValue = GetInputValue<int>("rhs");

            switch (condition)
            {
                case ConditionOperator.Equals:
                    return lhsValue == rhsValue;

                case ConditionOperator.NotEquals:
                    return lhsValue != rhsValue;

                case ConditionOperator.GreaterThan:
                    return lhsValue > rhsValue;

                case ConditionOperator.GreaterThanOrEqualTo:
                    return lhsValue >= rhsValue;

                case ConditionOperator.LessThan:
                    return lhsValue < rhsValue;

                case ConditionOperator.LessThanOrEqualTo:
                    return lhsValue <= rhsValue;

                default:
                    Debug.LogErrorFormat("Unhandled case {0} in IntComparerNode", condition);
                    return false;
            }
        }

        #endregion
    }
}

using Robbi.Logic;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [Serializable]
    public class LongValueCondition : ParameterizedValueCondition<long, LongValue, LongReference>
    {
        #region Condition Methods

        protected override bool Check()
        {
            switch (condition)
            {
                case ConditionOperator.Equals:
                    return value.Value == target.Value;

                case ConditionOperator.NotEquals:
                    return value.Value != target.Value;

                case ConditionOperator.LessThan:
                    return value.Value < target.Value;

                case ConditionOperator.LessThanOrEqualTo:
                    return value.Value <= target.Value;

                case ConditionOperator.GreaterThan:
                    return value.Value > target.Value;

                case ConditionOperator.GreaterThanOrEqualTo:
                    return value.Value >= target.Value;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in Long Condition", condition);
                    return false;
            }
        }

        #endregion
    }
}

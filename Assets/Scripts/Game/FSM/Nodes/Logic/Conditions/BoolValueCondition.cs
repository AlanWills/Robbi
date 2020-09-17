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
    public class BoolValueCondition : ParameterizedValueCondition<bool, BoolValue, BoolReference>
    {
        #region Condition Methods

        protected override bool Check()
        {
            switch (condition)
            {
                case ConditionOperator.Equals:
                    return value.value == target.Value;

                case ConditionOperator.NotEquals:
                    return value.value != target.Value;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in Bool Condition", condition);
                    return false;
            }
        }

        #endregion
    }
}

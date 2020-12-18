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
    public class Vector3IntValueCondition : ParameterizedValueCondition<Vector3Int, Vector3IntValue, Vector3IntReference>
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

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in Vector3Int Condition", condition);
                    return false;
            }
        }

        #endregion
    }
}

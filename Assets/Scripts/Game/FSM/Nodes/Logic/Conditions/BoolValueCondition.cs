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
    public class BoolValueCondition : ValueCondition
    {
        #region Properties and Fields

        public BoolValue value;
        public ConditionOperator condition;
        public BoolReference target;

        #endregion

        #region Init Methods

#if UNITY_EDITOR
        public override void Init_EditorOnly(IParameterContainer parameterContainer)
        {
            if (target == null)
            {
                target = parameterContainer.CreateParameter<BoolReference>(name + "_target");
            }
        }
#endif

        #endregion

        #region Condition Methods

        public override bool Check()
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

        #region ICopyable

        public override void CopyFrom(ValueCondition original)
        {
            BoolValueCondition boolValueCondition = original as BoolValueCondition;
            boolValueCondition.value = value;
            boolValueCondition.condition = condition;
            boolValueCondition.target = target;
        }

        #endregion
    }
}

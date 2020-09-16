using Robbi.Logic;
using Robbi.Objects;
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
    public abstract class ParameterizedValueCondition<T, TValue, TReference> : ValueCondition
        where TValue : ParameterValue<T>
        where TReference : ParameterReference<T, TValue, TReference>
    {
        #region Properties and Fields

        public TValue value;
        public ConditionOperator condition;
        public TReference target;

        #endregion

        #region Init Methods

#if UNITY_EDITOR
        public override void Init_EditorOnly(IParameterContainer parameterContainer)
        {
            if (target == null)
            {
                target = parameterContainer.CreateParameter<TReference>(name + "_target");
            }
        }

        public override void Cleanup_EditorOnly(IParameterContainer parameterContainer)
        {
            if (target != null)
            {
                parameterContainer.RemoveAsset(target);
            }
        }
#endif

        #endregion

        #region ICopyable

        public override void CopyFrom(ValueCondition original)
        {
            ParameterizedValueCondition<T, TValue, TReference> valueCondition = original as ParameterizedValueCondition<T, TValue, TReference>;
            valueCondition.value = value;
            valueCondition.condition = condition;
            valueCondition.target = target;
        }

        #endregion
    }
}

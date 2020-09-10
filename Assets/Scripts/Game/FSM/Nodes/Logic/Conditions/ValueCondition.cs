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
    public enum ConditionOperator
    { 
        Equals,
        NotEquals,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo
    }

    [Serializable]
    public abstract class ValueCondition : ScriptableObject, ICopyable<ValueCondition>
    {
#if UNITY_EDITOR
        public abstract void Init_EditorOnly(IParameterContainer parameterContainer);
#endif

        public abstract bool Check();

        #region ICopyable

        public abstract void CopyFrom(ValueCondition original);

        #endregion
    }
}

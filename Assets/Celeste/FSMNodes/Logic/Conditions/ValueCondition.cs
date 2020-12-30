using Celeste.Objects;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.FSM.Nodes.Logic.Conditions
{
    [Serializable]
    public abstract class ValueCondition : ScriptableObject, ICopyable<ValueCondition>
    {
#if UNITY_EDITOR
        public abstract void Init_EditorOnly(IParameterContainer parameterContainer);
        public abstract void Cleanup_EditorOnly(IParameterContainer parameterContainer);
#endif

        public abstract bool Check(object arg);

        #region Unity Methods

        private void OnEnable()
        {
            hideFlags = HideFlags.HideInHierarchy;
        }

        #endregion

        #region ICopyable

        public abstract void CopyFrom(ValueCondition original);

        #endregion
    }
}

using Robbi.Levels.Elements;
using Celeste.Parameters;
using System.Collections.Generic;
using UnityEngine;
using Celeste.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using System.ComponentModel;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("All Collection Targets Reached")]
    public class AllCollectionTargetsReached : Condition
    {
        #region Properties and Fields

        [SerializeField] private List<UIntValue> collectedAmounts = new List<UIntValue>();
        [SerializeField] private List<UIntValue> requiredAmounts = new List<UIntValue>();

        #endregion

#if UNITY_EDITOR
        public override void Init_EditorOnly(IParameterContainer parameterContainer) { }
        public override void Cleanup_EditorOnly(IParameterContainer parameterContainer) { }
#endif

        #region Check Methods

        public sealed override bool Check(object arg)
        {
            return Check();
        }

        private bool Check()
        {
            if (collectedAmounts.Count != requiredAmounts.Count)
            {
                Debug.LogAssertionFormat("Collection Target parameter mismatch.  Collected: {0}, Required: {1}", collectedAmounts.Count, requiredAmounts.Count);
                return false;
            }

            for (int i = 0; i < collectedAmounts.Count; ++i)
            {
                if (collectedAmounts[i].Value < requiredAmounts[i].Value)
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(Condition original) { }

        #endregion
    }
}

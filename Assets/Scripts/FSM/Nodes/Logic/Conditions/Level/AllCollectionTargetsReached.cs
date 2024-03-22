using Celeste.Parameters;
using System.Collections.Generic;
using UnityEngine;
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

        #region Check Methods

        protected override bool DoCheck()
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

        public override void CopyFrom(Condition original)
        {
            AllCollectionTargetsReached originalCondition = original as AllCollectionTargetsReached;
            collectedAmounts.AddRange(originalCondition.collectedAmounts);
            requiredAmounts.AddRange(originalCondition.requiredAmounts);
        }

        public override void SetVariable(object arg) { }
    }
}

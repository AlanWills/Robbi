using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Raisers/BoolEvent Raiser")]
    public class BoolEventRaiserNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public bool argument;

        public BoolEvent toRaise;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            bool _argument = GetInputValue(nameof(argument), argument);
            toRaise.Raise(_argument);
        }

        #endregion
    }
}

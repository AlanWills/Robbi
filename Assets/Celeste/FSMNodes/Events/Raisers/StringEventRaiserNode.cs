using Celeste.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Celeste/Events/Raisers/StringEvent Raiser")]
    public class StringEventRaiserNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public string argument;

        public StringEvent toRaise;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            string _argument = GetInputValue(nameof(argument), argument);
            toRaise.Raise(_argument);
        }

        #endregion
    }
}

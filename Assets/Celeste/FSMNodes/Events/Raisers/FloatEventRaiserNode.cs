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
    [CreateNodeMenu("Celeste/Events/Raisers/FloatEvent Raiser")]
    public class FloatEventRaiserNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public float argument;

        public FloatEvent toRaise;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            float _argument = GetInputValue(nameof(argument), argument);
            toRaise.Raise(_argument);
        }

        #endregion
    }
}

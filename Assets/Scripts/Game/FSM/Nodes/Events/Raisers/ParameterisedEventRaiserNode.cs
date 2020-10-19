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
    public abstract class ParameterisedEventRaiserNode<T, TEvent> : FSMNode where TEvent : ParameterisedEvent<T>
    {
        #region Properties and Fields

        [Input]
        public T argument;

        public TEvent toRaise;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            T _argument = GetInputValue(nameof(argument), argument);
            toRaise.Raise(_argument);
        }

        #endregion
    }
}

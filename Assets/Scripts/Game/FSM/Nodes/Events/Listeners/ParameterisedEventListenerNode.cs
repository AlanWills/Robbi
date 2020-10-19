﻿using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    public abstract class ParameterisedEventListenerNode<T, TEvent> : FSMNode, IEventListener<T> where TEvent : ParameterisedEvent<T>
    {
        #region Properties and Fields

        [Output]
        public T argument;

        public TEvent listenFor;

        private bool eventRaised = false;

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            eventRaised = false;
            listenFor.AddEventListener(this);
        }

        protected override FSMNode OnUpdate()
        {
            return eventRaised ? base.OnUpdate() : this;
        }

        protected override void OnExit()
        {
            base.OnExit();

            eventRaised = false;
            listenFor.RemoveEventListener(this);
        }

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            return argument;
        }

        #endregion

        #region IEventListener Implementation

        public void OnEventRaised(T argument)
        {
            eventRaised = true;
            this.argument = argument;
        }

        #endregion
    }
}

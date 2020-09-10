﻿using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Event Listener")]
    [NodeTint(0.8f, 0.9f, 0)]
    public class EventListenerNode : FSMNode, IEventListener
    {
        #region Properties and Fields

        public Event listenFor;

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
            return eventRaised ? base.OnUpdate() : null;
        }

        protected override void OnExit()
        {
            base.OnExit();

            eventRaised = false;
            listenFor.RemoveEventListener(this);
        }

        #endregion

        #region IEventListener Implementation

        public void OnEventRaised()
        {
            eventRaised = true;
        }

        #endregion
    }
}

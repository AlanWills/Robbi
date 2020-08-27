using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Event Listener Node")]
    public class EventListenerNode : FSMNode, IEventListener
    {
        #region Properties and Fields

        public Event listenFor;

        private bool eventRaised = false;

        #endregion

        public EventListenerNode()
        {
            AddDefaultInputPort();
            AddDefaultOutputPort();
        }

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            eventRaised = false;
            listenFor.AddEventListener(this);
        }

        protected override FSMNode OnUpdate()
        {
            return eventRaised ? GetConnectedNode(DEFAULT_OUTPUT_PORT_NAME) : null;
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

using Robbi.Events;
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
    [CreateNodeMenu("Robbi/Events/Listeners/Vector3IntEvent Listener")]
    [NodeTint(0.8f, 0.9f, 0)]
    public class Vector3IntEventListenerNode : FSMNode, IEventListener<Vector3Int>
    {
        #region Properties and Fields

        [Output]
        public Vector3Int argument;

        public Vector3IntEvent listenFor;

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

        public void OnEventRaised(Vector3Int argument)
        {
            eventRaised = true;
            this.argument = argument;
        }

        #endregion
    }
}

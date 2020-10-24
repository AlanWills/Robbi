using Robbi.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;
using static XNode.Node;

using Event = Robbi.Events.Event;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Listeners/Multi Event Listener")]
    [NodeTint(0.8f, 0.9f, 0)]
    public class MultiEventListenerNode : MultiEventNode
    {
        public MultiEventListenerNode()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);
        }

        #region FSM Runtime Methods

        protected override FSMNode OnUpdate()
        {
            FSMNode connectedNode = null;
            
            foreach (EventCondition eventCondition in this)
            {
                if (eventCondition.HasEventFired())
                {
                    if (connectedNode == null)
                    {
                        string eventConditionName = eventCondition.name;
                        argument = eventCondition.ConsumeEvent();

                        Debug.LogFormat("Name: {0} with Argument: {1} was consumed by MEL Node", eventConditionName, argument != null ? argument : "");
                        connectedNode = GetConnectedNode(eventConditionName);
                    }
                    else
                    {
                        Debug.LogFormat("Name: {0} silently consumed by MEL Node", eventCondition.name);
                        eventCondition.ConsumeEvent();
                    }
                }
            }

            return connectedNode != null ? connectedNode : this;
        }

        #endregion
    }
}

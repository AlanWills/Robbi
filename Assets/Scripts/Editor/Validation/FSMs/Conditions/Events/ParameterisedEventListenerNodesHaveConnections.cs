using Robbi.Events;
using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Logic;
using Robbi.Utils;
using RobbiEditor.Validation.FSM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace RobbiEditor.Validation.FSM.Conditions.Events
{
    public abstract class ParameterisedEventListenerNodesHaveConnections<T, TEvent> : IValidationCondition<FSMGraph>
         where TEvent : ParameterisedEvent<T>
    {
        public abstract string DisplayName { get; }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode fsmNode in fsmGraph.nodes)
            {
                if (fsmNode is ParameterisedEventListenerNode<T, TEvent>)
                {
                    valid &= fsmNode.CheckPortConnected(FSMNode.DEFAULT_OUTPUT_PORT_NAME, output);
                }
            }

            return valid;
        }
    }
}

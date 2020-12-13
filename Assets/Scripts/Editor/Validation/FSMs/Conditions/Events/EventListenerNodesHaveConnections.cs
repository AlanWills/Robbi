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
    public class EventListenerNodesHaveConnections : IValidationCondition<FSMGraph>
    {
        public string DisplayName { get { return "Event Listener Nodes Have Connections"; } }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode fsmNode in fsmGraph.nodes)
            {
                if (fsmNode is EventListenerNode)
                {
                    valid &= fsmNode.CheckPortConnected(FSMNode.DEFAULT_OUTPUT_PORT_NAME, output);
                }
            }

            return valid;
        }
    }
}

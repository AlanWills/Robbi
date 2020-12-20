using RobbiEditor.Validation.FSM.Utils;
using System.Text;
using Celeste.FSM;
using CelesteEditor.Validation;
using Celeste.FSM.Nodes.Events;

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

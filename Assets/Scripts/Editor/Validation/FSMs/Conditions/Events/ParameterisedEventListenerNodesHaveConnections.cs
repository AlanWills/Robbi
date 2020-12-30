using Celeste.Events;
using Celeste.FSM;
using Celeste.FSM.Nodes.Events;
using CelesteEditor.Validation;
using RobbiEditor.Validation.FSM.Utils;
using System.Text;

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

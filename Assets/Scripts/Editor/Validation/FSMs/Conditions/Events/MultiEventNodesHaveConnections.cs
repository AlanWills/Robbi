using Celeste.FSM;
using Celeste.FSM.Nodes.Events;
using CelesteEditor.Validation;
using RobbiEditor.Validation.FSM.Utils;
using System.Text;

namespace RobbiEditor.Validation.FSM.Conditions.Events
{
    public class MultiEventNodesHaveConnections : IValidationCondition<FSMGraph>
    {
        public string DisplayName { get { return "Multi Event Nodes Have Connections"; } }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode fsmNode in fsmGraph.nodes)
            {
                if (fsmNode is MultiEventNode)
                {
                    MultiEventNode multiEventListenerNode = fsmNode as MultiEventNode;

                    for (uint i = 0; i < multiEventListenerNode.NumEvents; ++i)
                    {
                        valid &= multiEventListenerNode.CheckPortConnected(multiEventListenerNode.GetEvent(i).name, output);
                    }
                }
            }

            return valid;
        }
    }
}

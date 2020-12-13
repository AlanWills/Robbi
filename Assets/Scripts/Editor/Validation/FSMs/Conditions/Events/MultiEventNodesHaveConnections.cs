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

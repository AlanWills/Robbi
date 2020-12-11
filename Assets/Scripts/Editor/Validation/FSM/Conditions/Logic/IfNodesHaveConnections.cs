using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Logic;
using Robbi.Utils;
using RobbiEditor.Validation.FSM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace RobbiEditor.Validation.FSM.Conditions.Logic
{
    public class IfNodesHaveConnections : IFSMValidationCondition
    {
        public string DisplayName { get { return "If Nodes Have Connections"; } }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode fsmNode in fsmGraph.nodes)
            {
                if (fsmNode is IfNode)
                {
                    IfNode ifNode = fsmNode as IfNode;

                    for (uint i = 0; i < ifNode.NumConditions; ++i)
                    {
                        valid &= ifNode.CheckPortConnected(ifNode.GetCondition(i).name, output);
                    }
                }
            }

            return valid;
        }
    }
}

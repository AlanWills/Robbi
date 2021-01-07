using RobbiEditor.Validation.FSM.Utils;
using System.Text;
using Celeste.FSM;
using CelesteEditor.Validation;
using Celeste.FSM.Nodes.Logic;

namespace RobbiEditor.Validation.FSM.Conditions.Logic
{
    public class IfNodesHaveConnections : IValidationCondition<FSMGraph>
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

                    valid &= ifNode.CheckPortConnected(IfNode.DEFAULT_OUTPUT_PORT_NAME, output);
                }
            }

            return valid;
        }
    }
}

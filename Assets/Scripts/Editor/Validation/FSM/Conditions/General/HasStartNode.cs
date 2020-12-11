using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobbiEditor.Validation.FSM.Conditions.General
{
    public class HasStartNode : IFSMValidationCondition, IFixableCondition
    {
        #region Properties and Fields

        public string DisplayName { get { return "Has Start Node"; } }

        #endregion

        #region IFixable

        public bool CanFix(FSMGraph fsmGraph)
        {
            return fsmGraph.nodes.Count > 0;
        }

        public void Fix(FSMGraph fsmGraph, StringBuilder output)
        {
            fsmGraph.startNode = fsmGraph.nodes[0] as FSMNode;
        }

        #endregion

        #region Validation

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            if (fsmGraph.startNode == null)
            {
                output.AppendLineFormat("{0} has no start node set", fsmGraph.name);
                return false;
            }

            return true;
        }

        #endregion
    }
}

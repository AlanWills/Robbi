using Celeste.Tools;
using System.Text;
using Celeste.FSM;
using CelesteEditor.Validation;

namespace RobbiEditor.Validation.FSM.Conditions.General
{
    public class HasStartNode : IValidationCondition<FSMGraph>, IFixableCondition<FSMGraph>
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

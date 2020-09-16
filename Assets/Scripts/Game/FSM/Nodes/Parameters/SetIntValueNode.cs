using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set Int Value")]
    [NodeWidth(250)]
    public class SetIntValueNode : FSMNode
    {
        #region Properties and Fields

        public IntValue intValue;
        public IntReference newValue;

        #endregion

        #region Add/Remove/Copy

        protected override void OnAddToGraph()
        {
            base.OnAddToGraph();

            if (newValue == null)
            {
                newValue = CreateParameter<IntReference>(name + "_sceneName");
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            RemoveParameter(newValue);
        }

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            SetIntValueNode setIntValueNode = original as SetIntValueNode;
            newValue = CreateParameter(setIntValueNode.newValue);
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            intValue.value = newValue.Value;
        }

        #endregion
    }
}

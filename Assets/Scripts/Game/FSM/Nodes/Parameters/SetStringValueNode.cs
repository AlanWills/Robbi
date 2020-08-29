using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set String Value")]
    public class SetStringValueNode : FSMNode
    {
        #region Properties and Fields

        public StringValue stringValue;
        public string newValue;

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            stringValue.value = newValue;
        }

        #endregion
    }
}

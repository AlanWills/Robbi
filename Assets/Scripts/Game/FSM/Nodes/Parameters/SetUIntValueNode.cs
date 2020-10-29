using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set UInt Value")]
    [NodeWidth(250)]
    public class SetUIntValueNode : SetValueNode<uint, UIntValue, UIntReference>
    {
        #region Properties and Fields

        public SetMode setMode = SetMode.Absolute;

        #endregion

        #region FSM Runtime

        protected override void SetValue(uint newValue)
        {
            value.value = setMode == SetMode.Absolute ? newValue : value.value + newValue;
        }

        #endregion
    }
}

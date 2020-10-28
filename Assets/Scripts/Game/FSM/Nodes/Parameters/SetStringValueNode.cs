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
    [NodeWidth(250)]
    public class SetStringValueNode : SetValueNode<string, StringValue, StringReference>
    {
        #region Properties and Fields

        public SetMode setMode = SetMode.Absolute;

        #endregion

        #region FSM Runtime

        protected override void SetValue(string newValue)
        {
            value.value = setMode == SetMode.Absolute ? newValue : value.value + newValue;
        }

        #endregion
    }
}

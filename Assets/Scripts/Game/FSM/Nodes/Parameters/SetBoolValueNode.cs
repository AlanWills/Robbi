using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set Bool Value")]
    [NodeWidth(250)]
    public class SetBoolValueNode : SetValueNode<bool, BoolValue, BoolReference>
    {
        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            value.value = newValue.Value;
        }

        #endregion
    }
}

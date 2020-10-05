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
    public class SetIntValueNode : SetValueNode<int, IntValue, IntReference>
    {
    }
}

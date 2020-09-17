using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    public class Vector3IntEventCondition : ParameterizedEventCondition<Vector3Int, Vector3IntEvent>
    {
    }
}

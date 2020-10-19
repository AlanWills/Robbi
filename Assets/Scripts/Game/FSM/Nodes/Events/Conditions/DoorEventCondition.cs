using Robbi.Events;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    public class DoorEventCondition : ParameterizedEventCondition<Door, DoorEvent>
    {
    }
}

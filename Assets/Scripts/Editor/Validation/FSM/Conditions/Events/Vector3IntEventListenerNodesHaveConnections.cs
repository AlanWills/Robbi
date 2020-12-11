using Robbi.Events;
using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Logic;
using Robbi.Utils;
using RobbiEditor.Validation.FSM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace RobbiEditor.Validation.FSM.Conditions.Events
{
    public class Vector3IntEventListenerNodesHaveConnections : ParameterisedEventListenerNodesHaveConnections<Vector3Int, Vector3IntEvent>
    {
        public override string DisplayName { get { return "Vector3Int Event Listener Nodes Have Connections"; } }
    }
}

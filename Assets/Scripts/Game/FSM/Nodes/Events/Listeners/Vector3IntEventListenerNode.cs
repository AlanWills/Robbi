﻿using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Listeners/Vector3IntEvent Listener")]
    [NodeTint(0.8f, 0.9f, 0)]
    public class Vector3IntEventListenerNode : ParameterisedEventListenerNode<Vector3Int, Vector3IntEvent>
    {
    }
}

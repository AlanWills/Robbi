using Celeste.Events;
using UnityEngine;

namespace RobbiEditor.Validation.FSM.Conditions.Events
{
    public class Vector3IntEventListenerNodesHaveConnections : ParameterisedEventListenerNodesHaveConnections<Vector3Int, Vector3IntEvent>
    {
        public override string DisplayName { get { return "Vector3Int Event Listener Nodes Have Connections"; } }
    }
}

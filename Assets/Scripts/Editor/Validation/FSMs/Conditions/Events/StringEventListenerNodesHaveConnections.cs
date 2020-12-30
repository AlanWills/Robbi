using Celeste.Events;

namespace RobbiEditor.Validation.FSM.Conditions.Events
{
    public class StringEventListenerNodesHaveConnections : ParameterisedEventListenerNodesHaveConnections<string, StringEvent>
    {
        public override string DisplayName { get { return "String Event Listener Nodes Have Connections"; } }
    }
}

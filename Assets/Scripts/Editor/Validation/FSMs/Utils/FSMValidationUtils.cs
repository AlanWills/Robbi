using Celeste.FSM;
using Celeste.Tools;
using System.Text;
using XNode;

namespace RobbiEditor.Validation.FSM.Utils
{
    public static class FSMValidationUtils
    {
        public static bool CheckPortConnected(this FSMNode fsmNode, string portName, StringBuilder output)
        {
            NodePort nodePort = fsmNode.GetOutputPort(portName);
            if (nodePort == null)
            {
                output.AppendLine($"{fsmNode.GetType().Name} {fsmNode.name} does not have port {portName}");
                return false;
            }

            return fsmNode.CheckPortConnected(nodePort, output);
        }

        public static bool CheckPortConnected(this FSMNode fsmNode, NodePort nodePort, StringBuilder output)
        {
            if (!nodePort.IsConnected)
            {
                output.AppendLine($"{fsmNode.GetType().Name} {fsmNode.name} does not have port {nodePort.fieldName}");
                return false;
            }

            return true;
        }
    }
}

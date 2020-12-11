using Robbi.FSM.Nodes;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace RobbiEditor.Validation.FSM.Utils
{
    public static class ValidationUtils
    {
        public static bool CheckPortConnected(this FSMNode fsmNode, string portName, StringBuilder output)
        {
            NodePort nodePort = fsmNode.GetOutputPort(portName);
            if (nodePort == null)
            {
                output.AppendLineFormat("{0} {1} does not have port {2}", fsmNode.GetType().Name, fsmNode.name, portName);
                return false;
            }

            return fsmNode.CheckPortConnected(nodePort, output);
        }

        public static bool CheckPortConnected(this FSMNode fsmNode, NodePort nodePort, StringBuilder output)
        {
            if (!nodePort.IsConnected)
            {
                output.AppendLineFormat("{0} {1} has port {2} which is not connected", fsmNode.GetType().Name, fsmNode.name, nodePort.fieldName);
                return false;
            }

            return true;
        }
    }
}

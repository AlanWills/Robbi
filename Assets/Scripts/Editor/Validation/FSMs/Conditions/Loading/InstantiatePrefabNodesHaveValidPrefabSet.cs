using Celeste.FSM;
using Celeste.FSM.Nodes.Loading;
using CelesteEditor.Tools;
using CelesteEditor.Validation;
using System.Text;
using UnityEngine;
using XNode;

namespace RobbiEditor.Validation.FSM.Conditions.Loading
{
    public class InstantiatePrefabNodesHaveValidPrefabSet : IValidationCondition<FSMGraph>
    {
        public string DisplayName { get { return "Instantiate Prefab Nodes Have Prefab"; } }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode node in fsmGraph.nodes)
            {
                if (node is InstantiatePrefabNode)
                {
                    InstantiatePrefabNode prefabNode = node as InstantiatePrefabNode;
                    
                    if (prefabNode.isAddressable)
                    {
                        // If the port name is connected it'll be evaluated at runtime, so we can't statically check it now
                        NodePort addressablePathPort = prefabNode.GetInputPort(nameof(prefabNode.addressablePath));
                        bool portNotConnected = addressablePathPort == null || !addressablePathPort.IsConnected;

                        if (portNotConnected && !AddressablesUtility.AddressableResourceExists<GameObject>(prefabNode.addressablePath))
                        {
                            valid = false;
                            output.AppendLine($"Scene Loader Node {node.name} has an invalid addressable path {prefabNode.addressablePath} set");
                        }
                    }
                    else
                    {
                        if (prefabNode.prefab == null)
                        {
                            valid = false;
                            output.AppendLine($"Interactable State Machine {node.name} has no states");
                        }
                    }
                }
            }

            return valid;
        }
    }
}

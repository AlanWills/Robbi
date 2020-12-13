using Robbi.FSM;
using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Loading;
using Robbi.Utils;
using RobbiEditor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;
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
                            output.AppendLineFormat("Instantiate Prefab Node {0} has an invalid addressable path {1} set", node.name, prefabNode.addressablePath);
                        }
                    }
                    else
                    {
                        if (prefabNode.prefab == null)
                        {
                            valid = false;
                            output.AppendLineFormat("Instantiate Prefab Node {0} has no prefab set", node.name);
                        }
                    }
                }
            }

            return valid;
        }
    }
}

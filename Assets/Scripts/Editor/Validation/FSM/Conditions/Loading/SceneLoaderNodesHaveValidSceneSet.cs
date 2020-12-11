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
using UnityEngine.SceneManagement;
using XNode;

namespace RobbiEditor.Validation.FSM.Conditions.Loading
{
    public class SceneLoaderNodesHaveValidSceneSet : IFSMValidationCondition
    {
        public string DisplayName { get { return "Scene Loader Nodes Have Valid Scene Set"; } }

        public bool Validate(FSMGraph fsmGraph, StringBuilder output)
        {
            bool valid = true;

            foreach (FSMNode node in fsmGraph.nodes)
            {
                if (node is SceneLoaderNode)
                {
                    SceneLoaderNode sceneLoaderNode = node as SceneLoaderNode;
                    
                    if (sceneLoaderNode.isAddressable)
                    {
                        // If the port name is connected it'll be evaluated at runtime, so we can't statically check it now
                        NodePort sceneNamePort = sceneLoaderNode.GetInputPort(nameof(sceneLoaderNode.sceneName));
                        bool portNotConnected = sceneNamePort == null || !sceneNamePort.IsConnected;
                        
                        if (!portNotConnected)
                        {
                            continue;
                        }
                        
                        // If we are using a parameter, again we cannot check this statically as it only has a value at runtime
                        if (sceneLoaderNode.sceneName.IsConstant)
                        {
                            if (!AddressablesUtility.AddressableResourceExists<Scene>(sceneLoaderNode.sceneName.Value))
                            {
                                valid = false;
                                output.AppendLineFormat("Scene Loader Node {0} has an invalid addressable path {1} set", node.name, sceneLoaderNode.sceneName.Value);
                            }
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(sceneLoaderNode.sceneName.Value))
                        {
                            valid = false;
                            output.AppendLineFormat("Scene Loader Node {0} has no scene name set", node.name);
                        }
                    }
                }
            }

            return valid;
        }
    }
}

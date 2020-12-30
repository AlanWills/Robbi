using Celeste.Tools;
using CelesteEditor.Tools;
using System.Text;
using UnityEngine.SceneManagement;
using XNode;
using Celeste.FSM;
using CelesteEditor.Validation;
using Celeste.FSM.Nodes;

namespace RobbiEditor.Validation.FSM.Conditions.Loading
{
    public class SceneLoaderNodesHaveValidSceneSet : IValidationCondition<FSMGraph>
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

using Robbi.Debugging.Commands;
using Robbi.Log;
using Robbi.FSM;
using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Testing
{
    public class ConsoleIntegrationTest : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 0)
            {
                output.Append("Expected an integration test name.");
                return false;
            }
            else if (parameters.Count == 1)
            {
                // Wait for the integration test to load
                AsyncOperationHandle<GameObject> integrationTest = Addressables.InstantiateAsync("Assets/Prefabs/Testing/IntegrationTest.prefab");
                integrationTest.Completed += (AsyncOperationHandle<GameObject> obj) =>
                {
                    if (obj.Result == null || !obj.IsValid())
                    {
                        HudLog.LogError("Failed to load integration test");
                    }
                    else
                    {
                        AsyncOperationHandle<FSMGraph> integrationTestFSM = Addressables.LoadAssetAsync<FSMGraph>(parameters[0]);
                        integrationTestFSM.Completed += (AsyncOperationHandle<FSMGraph> fsmObj) =>
                        {
                            if (fsmObj.Result == null || !fsmObj.IsValid())
                            {
                                HudLog.LogError("Failed to load integration test fsm");
                            }
                            else
                            {
                                obj.Result.name = fsmObj.Result.name;
                                GameObject.DontDestroyOnLoad(obj.Result);

                                FSMRuntime fsmRuntime = obj.Result.GetComponent<FSMRuntime>();
                                fsmRuntime.graph = fsmObj.Result;
                                fsmRuntime.Start();
                            }
                        };
                    }
                };
            }
            else if (parameters.Count == 2)
            {
                string operation = parameters[0];
                
                if (operation == "stop")
                {
                    string testName = parameters[1];
                    GameObject integrationTestGameObject = GameObject.Find(testName);

                    if (integrationTestGameObject != null)
                    {
                        IntegrationTest integrationTest = integrationTestGameObject.GetComponent<IntegrationTest>();
                        if (integrationTest != null)
                        {
                            integrationTest.StopTest();

                            output.AppendFormat("Stopping {0} IntegrationTest", testName);
                            return true;
                        }
                        else
                        {
                            output.AppendFormat("{0} is not an IntegrationTest", testName);
                            return false;
                        }
                    }
                    else
                    {
                        output.AppendFormat("Could not find IntegrationTest GameObject {0}", testName);
                        return false;
                    }
                }
                else
                {
                    output.AppendFormat("Invalid parameter {0}", operation);
                    return false;
                }
            }
            
            return true;
        }
    }
}

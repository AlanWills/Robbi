using Robbi.Debugging.Commands;
using Robbi.Debugging.Logging;
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
            if (parameters.Count < 1)
            {
                output.Append("Expected an integration test name.");
                return false;
            }

            // Wait for the integration test to load
            AsyncOperationHandle<GameObject> integrationTest = Addressables.InstantiateAsync("Assets/Prefabs/Testing/IntegrationTest.prefab");
            integrationTest.Completed += (AsyncOperationHandle<GameObject> obj) =>
            {
                if (obj.Result == null)
                {
                    HudLogger.LogError("Failed to load integration test");
                }
                else
                {
                    AsyncOperationHandle<FSMGraph> integrationTestFSM = Addressables.LoadAssetAsync<FSMGraph>(parameters[0]);
                    integrationTestFSM.Completed += (AsyncOperationHandle<FSMGraph> fsmObj) =>
                    {
                        if (fsmObj.Result == null)
                        {
                            HudLogger.LogError("Failed to load integration test fsm");
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
            
            return true;
        }
    }
}

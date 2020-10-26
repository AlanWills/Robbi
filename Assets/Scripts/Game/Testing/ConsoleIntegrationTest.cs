using Robbi.Debugging.Commands;
using Robbi.Debugging.Logging;
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
            AsyncOperationHandle<GameObject> integrationTest = Addressables.InstantiateAsync(parameters[0]);
            integrationTest.Completed += IntegrationTest_Completed;
            
            return true;
        }

        private void IntegrationTest_Completed(AsyncOperationHandle<GameObject> obj)
        {
            if (obj.Result == null)
            {
                HudLogger.LogError("Failed to run integration test");
            }
        }
    }
}

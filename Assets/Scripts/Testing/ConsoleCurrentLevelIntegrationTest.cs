using Robbi.Debugging.Commands;
using Robbi.Log;
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
    public class ConsoleCurrentLevelIntegrationTest : IDebugCommand
    {
        private ConsoleIntegrationTest consoleIntegrationTest = new ConsoleIntegrationTest();

        public bool Execute(List<string> parameters, StringBuilder output)
        {
            parameters.Add(string.Format("Level{0}IntegrationTest", LevelManager.Instance.CurrentLevel));
            return consoleIntegrationTest.Execute(parameters, output);
        }
    }
}

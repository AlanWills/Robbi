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
    public class ConsoleCurrentLevelIntegrationTest : IDebugCommand
    {
        private ConsoleIntegrationTest consoleIntegrationTest = new ConsoleIntegrationTest();

        public bool Execute(List<string> parameters, StringBuilder output)
        {
            LevelManager levelManager = LevelManager.Load();
            parameters.Add(string.Format("Level{0}IntegrationTest", levelManager.CurrentLevelIndex));
            return consoleIntegrationTest.Execute(parameters, output);
        }
    }
}

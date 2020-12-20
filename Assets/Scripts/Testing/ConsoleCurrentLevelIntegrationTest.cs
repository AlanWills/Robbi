using Celeste.Debugging.Commands;
using Robbi.Levels;
using System.Collections.Generic;
using System.Text;

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

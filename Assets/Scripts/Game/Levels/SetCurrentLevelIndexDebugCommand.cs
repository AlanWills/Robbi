using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels
{
    public class SetCurrentLevelIndexDebugCommand : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count < 1)
            {
                output.Append("Insufficient parameters to SetCurrentLevelIndexDebugCommand.");
                return false;
            }

            if (uint.TryParse(parameters[0], out uint result))
            {
                LevelManager levelManager = LevelManager.Load();
                levelManager.CurrentLevelIndex = result;
                levelManager.Save();

                output.Append(string.Format("Current Level Index successfully set to {0}", levelManager.CurrentLevelIndex));
                return true;
            }
            else
            {
                output.Append(string.Format("Invalid parameter {0} to SetCurrentLevelIndexDebugCommand.", parameters[0]));
                return false;
            }
        }
    }
}

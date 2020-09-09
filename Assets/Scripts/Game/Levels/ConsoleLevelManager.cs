using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels
{
    public class ConsoleLevelManager : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 0)
            {
                LevelManager levelManager = LevelManager.Load();
                output.Append(string.Format("Current Level Index is {0}", levelManager.CurrentLevelIndex));

                return true;
            }
            else if (parameters.Count == 1)
            {
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

            output.AppendFormat("Invalid parameters inputted into command.  Expected 0 or 1 numeric argument");
            return false;
        }
    }
}

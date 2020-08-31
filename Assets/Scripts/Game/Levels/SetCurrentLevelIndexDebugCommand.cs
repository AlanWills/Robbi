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
        public bool Execute(List<string> parameters)
        {
            if (parameters.Count < 1)
            {
                Debug.LogAssertion("Insufficient parameters to SetCurrentLevelIndexDebugCommand.");
                return false;
            }

            if (uint.TryParse(parameters[0], out uint result))
            {
                LevelManager levelManager = LevelManager.Load();
                levelManager.CurrentLevelIndex = result;
                levelManager.Save();

                return true;
            }
            else
            {
                Debug.LogAssertion(string.Format("Invalid parameter {0} to SetCurrentLevelIndexDebugCommand.", parameters[0]));
                return false;
            }
        }
    }
}

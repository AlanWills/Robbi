using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels
{
    public class GetCurrentLevelIndexDebugCommand : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            LevelManager levelManager = LevelManager.Load();
            output.Append(string.Format("Current Level Index is {0}", levelManager.CurrentLevelIndex));

            return true;
        }
    }
}

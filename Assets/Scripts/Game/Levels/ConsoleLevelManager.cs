﻿using Robbi.Debugging.Commands;
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
            LevelManager levelManager = LevelManager.Instance;

            if (parameters.Count == 0)
            {
                output.Append(string.Format("Current Level Index is {0}", levelManager.CurrentLevelIndex));
                output.Append(string.Format("Latest Level Index is {0}", levelManager.LatestLevelIndex));

                return true;
            }
            else if (parameters.Count == 1)
            {
                if (uint.TryParse(parameters[0], out uint result))
                {
                    levelManager.CurrentLevelIndex = result;
                    levelManager.Save();

                    output.Append(string.Format("Current Level Index successfully set to {0}", levelManager.CurrentLevelIndex));
                    return true;
                }
                else
                {
                    output.Append(string.Format("Invalid parameter {0} to set current level.", parameters[0]));
                    return false;
                }
            }
            else if (parameters.Count == 2)
            {
                if (parameters[0] == "latest")
                {
                    if (uint.TryParse(parameters[1], out uint result))
                    {
                        levelManager.LatestLevelIndex = result;
                        levelManager.Save();

                        output.Append(string.Format("Latest Level Index successfully set to {0}", levelManager.LatestLevelIndex));
                        return true;
                    }
                    else
                    {
                        output.Append(string.Format("Invalid parameter {0} to set latest level.", parameters[1]));
                        return false;
                    }
                }
                else
                {

                }
            }

            output.AppendFormat("Invalid parameters inputted into command");
            return false;
        }
    }
}

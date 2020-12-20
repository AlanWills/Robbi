﻿using Celeste.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celeste.Log
{
    public class ConsoleHudLog : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 0)
            {
                output.Append("Insufficient parameters.  Expected [severity|message]");
                return false;
            }
            else if (parameters.Count == 1)
            {
                HudLog.LogInfo(parameters[0]);
                return true;
            }
            else
            {
                if (parameters[0] == "i")
                {
                    HudLog.LogInfo(parameters[1]);
                    return true;
                }
                else if (parameters[0] == "w")
                {
                    HudLog.LogWarning(parameters[1]);
                    return true;
                }
                else if (parameters[0] == "e")
                {
                    HudLog.LogError(parameters[1]);
                    return true;
                }
                else
                {
                    output.AppendFormat("Invalid severity {0}.  Expected [i|w|e]");
                    return false;
                }
            }
        }
    }
}

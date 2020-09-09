using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Debugging.Logging
{
    public class ConsoleHudLogger : IDebugCommand
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
                HudLogger.LogInfo(parameters[0]);
                return true;
            }
            else
            {
                if (parameters[0] == "i")
                {
                    HudLogger.LogInfo(parameters[1]);
                    return true;
                }
                else if (parameters[0] == "w")
                {
                    HudLogger.LogWarning(parameters[1]);
                    return true;
                }
                else if (parameters[0] == "e")
                {
                    HudLogger.LogError(parameters[1]);
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

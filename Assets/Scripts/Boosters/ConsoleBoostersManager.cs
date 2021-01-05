using Celeste.Debugging.Commands;
using Celeste.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Boosters
{
    public class ConsoleBoostersManager : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 0)
            {
                output.AppendLineFormat("Num Waypoint Boosters: {0}", BoostersManager.Instance.NumWaypointBoosters);
                return true;
            }
            else if (parameters.Count == 2)
            {
                if (parameters[0] == "w")
                {
                    if (uint.TryParse(parameters[1], out uint result))
                    {
                        BoostersManager.Instance.NumWaypointBoosters = result;
                        BoostersManager.Instance.Save();

                        output.AppendLineFormat("Num Waypoint Boosters now {0}", BoostersManager.Instance.NumWaypointBoosters);
                        return true;
                    }
                    else
                    {
                        output.AppendLineFormat("Invalid number {0} entered when trying to modify waypoint boosters", parameters[1]);
                        return false;
                    }
                }
                else
                {
                    output.AppendLineFormat("Unrecognized parameter {0}", parameters[0]);
                    return false;
                }
            }

            output.AppendLine("Invalid number of parameters");
            return false;
        }
    }
}

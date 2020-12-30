using Celeste.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Movement
{
    public class ConsoleMovementManager : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 1)
            {
                if (int.TryParse(parameters[0], out int modifier))
                {
                    MovementDebug.SetPlaceableWaypoints(modifier);
                    output.AppendFormat("Placeable Waypoints set to {0}", modifier);
                    return true;
                }
                else
                {
                    output.AppendFormat("{0} is not a valid int value", parameters[0]);
                    return false;
                }
            }
            else if (parameters.Count == 2)
            {
                if (parameters[0] == "debug")
                {
                    if (parameters[1] == "on")
                    {
                        MovementDebug.SetDebugMovement(true);
                        output.Append("Debug Movement On");
                        return true;
                    }
                    else if (parameters[1] == "off")
                    {
                        MovementDebug.SetDebugMovement(false);
                        output.Append("Debug Movement Off");
                        return true;
                    }
                    else
                    {
                        output.AppendFormat("Unrecognised parameter {0} to debug movement", parameters[1]);
                        return false;
                    }
                }
                else
                {
                    output.AppendFormat("Unrecognised parameter {0}", parameters[0]);
                    return false;
                }
            }

            output.AppendFormat("Invalid parameters inputted into command");
            return false;
        }
    }
}

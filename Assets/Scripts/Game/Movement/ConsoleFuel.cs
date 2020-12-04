using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Movement
{
    public class ConsoleFuel : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 1)
            {
                if (int.TryParse(parameters[0], out int modifier))
                {
                    MovementDebug.AddFuel(modifier);
                    output.AppendFormat("Added {0} fuel", modifier);
                    return true;
                }
                else
                {
                    output.AppendFormat("{0} is not a valid int value", parameters[0]);
                    return false;
                }
            }

            output.AppendFormat("Invalid parameters inputted into command");
            return false;
        }
    }
}

using Celeste.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Options
{
    public class ConsoleOptionsManager : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            if (parameters.Count == 2)
            {
                string command = parameters[0];
                if (command == "ds")
                {
                    if (float.TryParse(parameters[1], out float speed))
                    {
                        OptionsManager.Instance.DragSpeed = speed;
                        OptionsManager.Save();
                        output.AppendLine(string.Format("Drag Speed set to {0}", speed));
                        return true;
                    }
                    else
                    {
                        output.AppendLine(string.Format("Invalid value {0} inputted into Set Drag Speed", parameters[1]));
                        return false;
                    }
                }
                else if (command == "zs")
                {
                    if (float.TryParse(parameters[1], out float speed))
                    {
                        OptionsManager.Instance.ZoomSpeed = speed;
                        OptionsManager.Save();
                        output.AppendLine(string.Format("Zoom Speed set to {0}", speed));
                        return true;
                    }
                    else
                    {
                        output.AppendLine(string.Format("Invalid value {0} inputted into Set Zoom Speed", parameters[1]));
                        return false;
                    }
                }
                else if (command == "miz")
                {
                    if (float.TryParse(parameters[1], out float minZoom))
                    {
                        OptionsManager.Instance.MinZoom = minZoom;
                        OptionsManager.Save();
                        output.AppendLine(string.Format("Min Zoom set to {0}", minZoom));
                        return true;
                    }
                    else
                    {
                        output.AppendLine(string.Format("Invalid value {0} inputted into Set Min Zoom", parameters[1]));
                        return false;
                    }
                }
                else if (command == "maz")
                {
                    if (float.TryParse(parameters[1], out float maxZoom))
                    {
                        OptionsManager.Instance.MaxZoom = maxZoom;
                        OptionsManager.Save();
                        output.AppendLine(string.Format("Max Zoom set to {0}", maxZoom));
                        return true;
                    }
                    else
                    {
                        output.AppendLine(string.Format("Invalid value {0} inputted into Set Max Zoom", parameters[1]));
                        return false;
                    }
                }
                else
                {
                    output.AppendLine(string.Format("Invalid command {0}", parameters[1]));
                    return false;
                }
            }

            output.AppendLine("Invalid parameters inputted into command");
            return false;
        }
    }
}

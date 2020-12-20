using Robbi.Debugging.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.App
{
    public class ConsoleApplication : IDebugCommand
    {
        public bool Execute(List<string> parameters, StringBuilder output)
        {
            output.AppendLine("Is Mobile: " + ApplicationInfo.IsMobile);
            output.AppendLine("PDP: " + Application.persistentDataPath);
            return true;
        }
    }
}

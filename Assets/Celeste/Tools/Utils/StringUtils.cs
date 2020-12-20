using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Utils
{
    public static class StringUtils
    {
        public static void AppendLineFormat(this StringBuilder stringBuilder, string format, params string[] args)
        {
            stringBuilder.AppendFormat(format, args);
            stringBuilder.AppendLine();
        }
    }
}

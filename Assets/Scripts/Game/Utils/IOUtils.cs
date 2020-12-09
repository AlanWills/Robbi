using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.Utils
{
    public static class IOUtils
    {
#if UNITY_WEBGL
        [DllImport("__Internal")]
        public static extern void WebGLOnly_SyncFiles();
#endif
    }
}

using Robbi.Constants;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Utils
{
    public static class AppUtils
    {
        public static void GetIsEditor(this BoolValue isEditor)
        {
            if (isEditor != null)
            {
                isEditor.Value = Application.isEditor;
                Debug.LogFormat("isEditor set to {0}", isEditor.Value);
            }
        }

        public static void GetIsMobile(this BoolValue isMobile)
        {
            if (isMobile != null)
            {
                isMobile.Value = Application.isMobilePlatform;
                Debug.LogFormat("isMobile set to {0}", isMobile.Value);
            }
        }

        public static void GetIsDebugBuild(this BoolValue isDebugBuild)
        {
            if (isDebugBuild != null)
            {
                TextAsset textAsset = Resources.Load<TextAsset>(DebugConstants.IS_DEBUG_BUILD_FILE);
                isDebugBuild.Value = textAsset != null && textAsset.text == "1";
                Debug.LogFormat("IS_DEBUG_BUILD_FILE file {0} ", textAsset != null ? "found with contents " + textAsset.text : "not found");
                Debug.LogFormat("isDebugBuild set to {0}", isDebugBuild.Value);
            }
        }
    }
}

using RobbiEditor.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RobbiEditor.BuildSystem
{
    public static class BuildPlayer
    {
        [MenuItem("Robbi/Builds/Android Debug")]
        public static void BuildAndroidDebug()
        {
            AndroidSettings.Debug.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/Android Release")]
        public static void BuildAndroidRelease()
        {
            AndroidSettings.Release.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/Windows Debug")]
        public static void BuildWindowsDebug()
        {
            WindowsSettings.Debug.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/Windows Release")]
        public static void BuildWindowsRelease()
        {
            WindowsSettings.Release.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/iOS Debug")]
        public static void BuildiOSDebug()
        {
            iOSSettings.Debug.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/iOS Release")]
        public static void BuildiOSRelease()
        {
            iOSSettings.Release.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/HTML5 Debug")]
        public static void BuildHTMLDebug()
        {
            HTML5Settings.Debug.BuildPlayer();
        }

        [MenuItem("Robbi/Builds/HTML5 Release")]
        public static void BuildHTML5Release()
        {
            HTML5Settings.Release.BuildPlayer();
        }
    }
}

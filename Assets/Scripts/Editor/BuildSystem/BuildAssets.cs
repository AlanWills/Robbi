using Robbi.Levels;
using RobbiEditor.Platform;
using RobbiEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RobbiEditor.BuildSystem
{
    public static class BuildAssets
    {
        [MenuItem("Robbi/Assets/Build/Debug Android Assets")]
        public static void BuildDebugAndroidAssets()
        {
            AndroidSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release Android Assets")]
        public static void BuildReleaseAndroidAssets()
        {
            AndroidSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug Windows Assets")]
        public static void BuildDebugWindowsAssets()
        {
            WindowsSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release Windows Assets")]
        public static void BuildReleaseWindowsAssets()
        {
            WindowsSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug iOS Assets")]
        public static void BuildDebugiOSAssets()
        {
            iOSSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release iOS Assets")]
        public static void BuildReleaseiOSAssets()
        {
            iOSSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug HTML5 Assets")]
        public static void BuildDebugHTML5Assets()
        {
            HTML5Settings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release HTML5 Assets")]
        public static void BuildReleaseHTML5Assets()
        {
            HTML5Settings.Release.BuildAssets();
        }
    }
}

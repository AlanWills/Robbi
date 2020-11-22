using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace RobbiEditor.BuildSystem
{
    public static class BuildVersion
    {
        [MenuItem("Robbi/Builds/Version/Bump Android")]
        public static void BumpAndroidVersion()
        {
            Version version = ParseVersion(PlayerSettings.Android.bundleVersionCode);
            PlayerSettings.Android.bundleVersionCode = version.Major * 10000 + version.Minor * 100 + version.Build + 1;
            PlayerSettings.bundleVersion = version.ToString();
        }

        [MenuItem("Robbi/Builds/Version/Bump Windows")]
        public static void BumpWindowsVersion()
        {
            // Bump windows version number - yes we're using the mac version number, but windows doesn't have a unique one apparently
            Version version = ParseVersion(PlayerSettings.macOS.buildNumber);
            version = new Version(version.Major, version.Minor, version.Build + 1);
            PlayerSettings.macOS.buildNumber = version.ToString();
            PlayerSettings.bundleVersion = version.ToString();
        }

        [MenuItem("Robbi/Builds/Version/Bump iOS")]
        public static void BumpiOSVersion()
        {
            Version version = ParseVersion(PlayerSettings.iOS.buildNumber);
            version = new Version(version.Major, version.Minor, version.Build + 1);
            PlayerSettings.iOS.buildNumber = version.ToString();
            PlayerSettings.bundleVersion = version.ToString();
        }

        public static Version ParseVersion(int bundleVersionCode)
        {
            int major = bundleVersionCode / 10000;
            int minor = (bundleVersionCode - major * 10000) / 100;
            int patch = bundleVersionCode - major * 10000 - minor * 100;

            return new Version(major, minor, patch);
        }

        public static Version ParseVersion(string bundleString)
        {
            return new Version(bundleString);
        }
    }
}

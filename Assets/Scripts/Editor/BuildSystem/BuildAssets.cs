using CelesteEditor.Platform;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Application;

namespace RobbiEditor.BuildSystem
{
    public static class BuildAssets
    {
        [MenuItem("Robbi/Assets/Build/Debug Android Assets")]
        public static void BuildDebugAndroidAssets()
        {
            AndroidSettings.Debug.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Release Android Assets")]
        public static void BuildReleaseAndroidAssets()
        {
            AndroidSettings.Release.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Debug Windows Assets")]
        public static void BuildDebugWindowsAssets()
        {
            WindowsSettings.Debug.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Release Windows Assets")]
        public static void BuildReleaseWindowsAssets()
        {
            WindowsSettings.Release.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Debug iOS Assets")]
        public static void BuildDebugiOSAssets()
        {
            iOSSettings.Debug.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Release iOS Assets")]
        public static void BuildReleaseiOSAssets()
        {
            iOSSettings.Release.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Debug HTML5 Assets")]
        public static void BuildDebugHTML5Assets()
        {
            HTML5Settings.Debug.BuildAssetsAndExit();
        }

        [MenuItem("Robbi/Assets/Build/Release HTML5 Assets")]
        public static void BuildReleaseHTML5Assets()
        {
            HTML5Settings.Release.BuildAssetsAndExit();
        }

        private static void BuildAssetsAndExit(this PlatformSettings platformSettings)
        {
            string exceptionMessage = "";
            bool hasThrownAnException = false;
            LogCallback logDelegate = (string logString, string stackTrace, LogType type) =>
            {
                if (type == LogType.Exception)
                {
                    hasThrownAnException = true;
                    exceptionMessage = logString;
                }
            };
            Application.logMessageReceived += logDelegate;
            platformSettings.BuildAssets();
            Application.logMessageReceived -= logDelegate;

            if (hasThrownAnException)
            {
                Debug.LogErrorFormat("Exception thrown during asset building: {0}", exceptionMessage);
            }

            if (Application.isBatchMode)
            {
                // 0 = everything OK
                // 1 = everything NOT OK
                EditorApplication.Exit(hasThrownAnException ? 0 : 1);
            }
            else
            {
                EditorUtility.DisplayDialog("Asset Build result", string.Format("Assets Built {0}", hasThrownAnException ? "Successfully" : "Unsuccessfully"), "OK");
            }
        }
    }
}

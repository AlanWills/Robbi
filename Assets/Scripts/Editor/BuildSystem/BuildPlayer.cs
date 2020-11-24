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
using static RobbiEditor.BuildSystem.BuildVersion;

namespace RobbiEditor.BuildSystem
{
    public static class BuildPlayer
    {
        [MenuItem("Robbi/Builds/Android Debug")]
        public static void BuildAndroidDebug()
        {
            EditorUserBuildSettings.development = true;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.StrictMode;
            Version version = ParseVersion(PlayerSettings.Android.bundleVersionCode);

            if (Build(buildPlayerOptions, BuildTargetGroup.Android, BuildTarget.Android, "Builds/Android", ".apk", version))
            {
                BumpAndroidVersion();
            }
            else if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Builds/Windows Debug")]
        public static void BuildWindowsDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;
            Version version = ParseVersion(PlayerSettings.macOS.buildNumber);

            if (Build(buildPlayerOptions, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, "Builds/Windows", ".exe", version))
            {
                BumpWindowsVersion();
            }
            else if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Builds/iOS Debug")]
        public static void BuildiOSDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            if (Build(buildPlayerOptions, BuildTargetGroup.iOS, BuildTarget.iOS, "Builds/iOS", "", ParseVersion(PlayerSettings.iOS.buildNumber)))
            {
                BumpiOSVersion();
            }
            else if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        private static bool Build(
            BuildPlayerOptions buildPlayerOptions,
            BuildTargetGroup buildTargetGroup, 
            BuildTarget buildTarget,
            string buildDirectory,
            string extension,
            Version newVersion)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

            buildPlayerOptions.locationPathName = string.Format("{0}/Robbi-{1}{2}", buildDirectory, newVersion.ToString(), extension);
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = buildTarget;
            buildPlayerOptions.targetGroup = buildTargetGroup;

            PlayerSettings.bundleVersion = newVersion.ToString();
            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            bool success = buildReport != null && buildReport.summary.result == BuildResult.Succeeded;

            if (success)
            {
                File.WriteAllText(Path.Combine(buildDirectory, "BUILD_LOCATION.txt"), buildPlayerOptions.locationPathName);
            }

            return success;
        }
    }
}

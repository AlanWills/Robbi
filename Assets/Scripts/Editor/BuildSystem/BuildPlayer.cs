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
            EditorUserBuildSettings.development = true;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.StrictMode;

            Build(buildPlayerOptions, AndroidSettings.Instance);
        }

        [MenuItem("Robbi/Builds/Windows Debug")]
        public static void BuildWindowsDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            Build(buildPlayerOptions, WindowsSettings.Instance);
        }

        [MenuItem("Robbi/Builds/iOS Debug")]
        public static void BuildiOSDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            Build(buildPlayerOptions, iOSSettings.Instance);
        }

        private static void Build(
            BuildPlayerOptions buildPlayerOptions,
            PlatformSettings platformSettings)
        {
            platformSettings.Switch();

            string buildDirectory = platformSettings.BuildDirectory;
            string outputName = platformSettings.OutputName;

            Debug.LogFormat("Build Directory: {0}", buildDirectory);
            Debug.LogFormat("Output Name: {0}", outputName);

            buildPlayerOptions.locationPathName = Path.Combine(buildDirectory, outputName);
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = platformSettings.BuildTarget;
            buildPlayerOptions.targetGroup = platformSettings.BuildTargetGroup;

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            bool success = buildReport != null && buildReport.summary.result == BuildResult.Succeeded;

            if (success)
            {
                File.WriteAllText(Path.Combine(buildDirectory, "BUILD_LOCATION.txt"), buildPlayerOptions.locationPathName);
                platformSettings.BumpVersion();
            }
            else if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }
    }
}

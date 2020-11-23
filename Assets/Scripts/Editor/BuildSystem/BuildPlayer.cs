using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
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

            Build(buildPlayerOptions, BuildTargetGroup.Android, BuildTarget.Android, string.Format("Builds/Android/Robbi-{0}.apk", version), version);
            BumpAndroidVersion();
        }

        [MenuItem("Robbi/Builds/Windows Debug")]
        public static void BuildWindowsDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;
            Version version = ParseVersion(PlayerSettings.macOS.buildNumber);

            Build(buildPlayerOptions, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, string.Format("Builds/Windows/Robbi-{0}.exe", version), version);
            BumpWindowsVersion();
        }

        [MenuItem("Robbi/Builds/iOS Debug")]
        public static void BuildiOSDebug()
        {
            EditorUserBuildSettings.development = true;
            EditorUserBuildSettings.iOSBuildConfigType = iOSBuildType.Debug;

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            Build(buildPlayerOptions, BuildTargetGroup.iOS, BuildTarget.iOS, "Builds/iOS", ParseVersion(PlayerSettings.iOS.buildNumber));
            BumpiOSVersion();
        }

        private static void Build(
            BuildPlayerOptions buildPlayerOptions,
            BuildTargetGroup buildTargetGroup, 
            BuildTarget buildTarget,
            string locationPathName,
            Version newVersion)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

            buildPlayerOptions.locationPathName = locationPathName;
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = buildTarget;
            buildPlayerOptions.targetGroup = buildTargetGroup;

            PlayerSettings.bundleVersion = newVersion.ToString();
            Environment.SetEnvironmentVariable("BUILD_VERSION", newVersion.ToString());
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}

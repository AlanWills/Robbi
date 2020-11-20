using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace RobbiEditor.BuildSystem
{
    public static class BuildPlayer
    {
        [MenuItem("Robbi/Builds/Android Debug")]
        public static void BuildAndroidDebug()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.StrictMode;
            
            Build(buildPlayerOptions, BuildTargetGroup.Android, BuildTarget.Android, "Builds/Android/Robbi.apk");
        }

        [MenuItem("Robbi/Builds/Windows Debug")]
        public static void BuildWindowsDebug()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            Build(buildPlayerOptions, BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64, "Builds/Windows/Robbi.exe");
        }

        [MenuItem("Robbi/Builds/iOS Debug")]
        public static void BuildiOSDebug()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            Build(buildPlayerOptions, BuildTargetGroup.iOS, BuildTarget.iOS, "Builds/iOS");
        }

        private static void Build(
            BuildPlayerOptions buildPlayerOptions,
            BuildTargetGroup buildTargetGroup, 
            BuildTarget buildTarget,
            string locationPathName)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);

            buildPlayerOptions.locationPathName = locationPathName;
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = buildTarget;
            buildPlayerOptions.targetGroup = buildTargetGroup;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}

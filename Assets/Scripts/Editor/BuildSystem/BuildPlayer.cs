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
        public static void BuildAndroidDebug()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;

            BuildAndroid(buildPlayerOptions);
        }

        private static void BuildAndroid(BuildPlayerOptions buildPlayerOptions)
        {
            buildPlayerOptions.locationPathName = "Builds/Robbi.apk";
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.targetGroup = BuildTargetGroup.Android;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}

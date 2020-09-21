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
        public static void BuildAndroid()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.locationPathName = Environment.GetCommandLineArgs().Last();
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.targetGroup = BuildTargetGroup.Android;

            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }
    }
}

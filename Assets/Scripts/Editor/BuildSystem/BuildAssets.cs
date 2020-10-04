using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace RobbiEditor.BuildSystem
{
    public static class BuildAssets
    {
        [MenuItem("Robbi/Assets/Build Android Assets")]
        public static void BuildAndroidAssets()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            AddressableAssetSettings.BuildPlayerContent();
        }

        [MenuItem("Robbi/Assets/Build Windows Assets")]
        public static void BuildWindowsAssets()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            AddressableAssetSettings.BuildPlayerContent();
        }
    }
}

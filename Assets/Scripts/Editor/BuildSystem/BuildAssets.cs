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
        public static void BuildAndroidAssets()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Android, BuildTarget.Android);
            AddressableAssetSettings.BuildPlayerContent();
        }
    }
}

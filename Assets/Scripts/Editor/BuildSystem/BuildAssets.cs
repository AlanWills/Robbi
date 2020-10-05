using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RobbiEditor.BuildSystem
{
    public static class BuildAssets
    {
        [MenuItem("Robbi/Assets/Build Android Assets")]
        public static void BuildAndroidAssets()
        {
            Build(BuildTargetGroup.Android, BuildTarget.Android);
        }

        [MenuItem("Robbi/Assets/Update Android Assets")]
        public static void UpdateAndroidAssets()
        {
            Update(BuildTargetGroup.Android, BuildTarget.Android);
        }

        [MenuItem("Robbi/Assets/Build Windows Assets")]
        public static void BuildWindowsAssets()
        {
            Build(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Robbi/Assets/Update Windows Assets")]
        public static void UpdateWindowsAssets()
        {
            Update(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows64);
        }

        [MenuItem("Robbi/Assets/Clear Asset Cache")]
        public static void ClearAssetCache()
        {
            Debug.Assert(Caching.ClearCache(), "Error clearing asset cache");
        }

        private static void Build(BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            SetProfileId("AWS");

            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);
            AddressableAssetSettings.BuildPlayerContent();
        }

        private static void Update(BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            SetProfileId("AWS");

            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);
            string contentStatePath = ContentUpdateScript.GetContentStateDataPath(false);
            ContentUpdateScript.BuildContentUpdate(AddressableAssetSettingsDefaultObject.Settings, contentStatePath);
        }

        private static void SetProfileId(string profile)
        {
            AddressableAssetProfileSettings profileSettings = AddressableAssetSettingsDefaultObject.Settings.profileSettings;
            string profileId = profileSettings.GetProfileId(profile);
            AddressableAssetSettingsDefaultObject.Settings.activeProfileId = profileId;
        }
    }
}

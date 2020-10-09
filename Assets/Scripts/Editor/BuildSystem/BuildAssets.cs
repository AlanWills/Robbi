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
            Debug.Log("Beginning to build content");

            bool result = EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);
            Debug.Log(result ? string.Format("Successfully switched to {0}", buildTarget) : string.Format("Failed to switch to {0}", buildTarget));
            Debug.LogFormat("Active build target is {0}", EditorUserBuildSettings.activeBuildTarget);
            Debug.LogFormat("Active Profile Id: {0}", AddressableAssetSettingsDefaultObject.Settings.activeProfileId);

            AddressableAssetSettings.BuildPlayerContent();

            Debug.Log("Finished building content");
        }

        private static void Update(BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            Debug.Log("Beginning to update content");

            bool result = EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);
            
            Debug.Log(result ? string.Format("Successfully switched to {0}", buildTarget) : string.Format("Failed to switch to {0}", buildTarget));
            Debug.LogFormat("Active build target is {0}", EditorUserBuildSettings.activeBuildTarget);
            Debug.LogFormat("Active Profile Id: {0}", AddressableAssetSettingsDefaultObject.Settings.activeProfileId);

            string contentStatePath = ContentUpdateScript.GetContentStateDataPath(false);
            AddressableAssetBuildResult buildResult = ContentUpdateScript.BuildContentUpdate(AddressableAssetSettingsDefaultObject.Settings, contentStatePath);

            Debug.LogFormat("Finished updating content{0}", string.IsNullOrEmpty(buildResult.Error) ? "" : " with error: " + buildResult.Error);
        }

        private static void SetProfileId(string profile)
        {
            Debug.Assert(AddressableAssetSettingsDefaultObject.SettingsExists, "AddressableAssetSettingsDefaultObject does not exist");
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            Debug.Assert(settings != null, "AddressableAssetSettingsDefaultObject is null");

            if (settings.profileSettings != null)
            {
                settings.activeProfileId = settings.profileSettings.GetProfileId(profile);
            }

            Debug.LogFormat("Active Profile Id: {0}", settings.activeProfileId);
        }
    }
}

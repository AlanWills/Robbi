using RobbiEditor.Platform;
using RobbiEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
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
        [MenuItem("Robbi/Assets/Build/Android Assets")]
        public static void BuildAndroidAssets()
        {
            Build(AndroidSettings.Instance);
        }

        [MenuItem("Robbi/Assets/Update/Android Assets")]
        public static void UpdateAndroidAssets()
        {
            if (!Update(AndroidSettings.Instance) && Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Assets/Build/Windows Assets")]
        public static void BuildWindowsAssets()
        {
            Build(WindowsSettings.Instance);
        }

        [MenuItem("Robbi/Assets/Update/Windows Assets")]
        public static void UpdateWindowsAssets()
        {
            if (!Update(WindowsSettings.Instance) && Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Assets/Build/iOS Assets")]
        public static void BuildiOSAssets()
        {
            Build(iOSSettings.Instance);
        }

        [MenuItem("Robbi/Assets/Update/iOS Assets")]
        public static void UpdateiOSAssets()
        {
            if (Update(iOSSettings.Instance) && Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Assets/Clear Asset Cache")]
        public static void ClearAssetCache()
        {
            Debug.Assert(Caching.ClearCache(), "Error clearing asset cache");
        }

        [MenuItem("Robbi/Assets/Prepare Assets")]
        public static void PrepareAssets()
        {
            CompressTilemaps.MenuItem();
            FindInteractables.MenuItem();
            SetAddressablePaths.MenuItem();
        }

        private static void Build(PlatformSettings platformSettings)
        {
            PreBuildSteps(platformSettings);

            Debug.Log("Beginning to build content");

            AddressableAssetSettings.BuildPlayerContent();

            StringBuilder locationInfo = new StringBuilder();
            locationInfo.AppendFormat("ASSETS_SOURCE=\"{0}\"", platformSettings.AddressablesBuildDirectory);
            locationInfo.AppendLine();
            locationInfo.AppendFormat("ASSETS_DESTINATION=\"{0}\"", platformSettings.AddressablesLoadDirectory);
            File.WriteAllText(Path.Combine(new DirectoryInfo(platformSettings.AddressablesBuildDirectory).Parent.FullName, "ASSETS_ENV_VARS.txt"), locationInfo.ToString());

            Debug.Log("Finished building content");
        }

        private static bool Update(PlatformSettings platformSettings)
        {
            PreBuildSteps(platformSettings);

            Debug.Log("Beginning to update content");

            string contentStatePath = ContentUpdateScript.GetContentStateDataPath(false);
            Debug.LogFormat("Using content state path {0}", contentStatePath);
            AddressableAssetBuildResult buildResult = ContentUpdateScript.BuildContentUpdate(AddressableAssetSettingsDefaultObject.Settings, contentStatePath);
            
            if (buildResult != null)
            {
                Debug.LogFormat("Finished updating content{0}", string.IsNullOrEmpty(buildResult.Error) ? "" : " with error: " + buildResult.Error);
            }
            else
            {
                Debug.LogFormat("Finished updating content with no build result");
            }

            return buildResult != null && string.IsNullOrEmpty(buildResult.Error);
        }

        private static void PreBuildSteps(PlatformSettings platformSettings)
        {
            Debug.Log("Beginning Pre Build steps");

            PrepareAssets();
            SetAddressableAssetSettings();
            SetProfileId("AWS");
            platformSettings.Switch();

            Debug.Log("Finished Pre Build steps");
        }

        private static void SetAddressableAssetSettings()
        {
            if (AddressableAssetSettingsDefaultObject.Settings == null)
            {
                Debug.Log("Loading settings from asset database");
                AddressableAssetSettingsDefaultObject.Settings = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(AddressableAssetSettingsDefaultObject.DefaultAssetPath);
            }

            Debug.Assert(AddressableAssetSettingsDefaultObject.Settings != null, "AddressableAssetSettingsDefaultObject is null");
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

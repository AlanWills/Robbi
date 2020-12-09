using Robbi.Levels;
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
        [MenuItem("Robbi/Assets/Build/Debug Android Assets")]
        public static void BuildDebugAndroidAssets()
        {
            AndroidSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release Android Assets")]
        public static void BuildReleaseAndroidAssets()
        {
            AndroidSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug Windows Assets")]
        public static void BuildDebugWindowsAssets()
        {
            WindowsSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release Windows Assets")]
        public static void BuildReleaseWindowsAssets()
        {
            WindowsSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug iOS Assets")]
        public static void BuildDebugiOSAssets()
        {
            iOSSettings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release iOS Assets")]
        public static void BuildReleaseiOSAssets()
        {
            iOSSettings.Release.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Debug HTML5 Assets")]
        public static void BuildDebugHTML5Assets()
        {
            HTML5Settings.Debug.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Build/Release HTML5 Assets")]
        public static void BuildReleaseHTML5Assets()
        {
            HTML5Settings.Release.BuildAssets();
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
            FindCollectables.MenuItem();
            SetAddressablePaths.MenuItem();
            SetCurrentLevelToZero();
        }

        public static void PreBuildSteps()
        {
            Debug.Log("Beginning Pre Build steps");

            PrepareAssets();
            SetAddressableAssetSettings();
            SetProfileId("AWS");

            Debug.Log("Finished Pre Build steps");
        }

        public static void SetAddressableAssetSettings()
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

        private static void SetCurrentLevelToZero()
        {
            LevelManager.EditorOnly_Load().CurrentLevelIndex_DefaultValue = 0;
            AssetDatabase.SaveAssets();
        }
    }
}

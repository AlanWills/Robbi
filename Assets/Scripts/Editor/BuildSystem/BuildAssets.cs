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
            AndroidSettings.Instance.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Update/Android Assets")]
        public static void UpdateAndroidAssets()
        {
            if (!AndroidSettings.Instance.UpdateAssets() && Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Assets/Build/Windows Assets")]
        public static void BuildWindowsAssets()
        {
            WindowsSettings.Instance.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Update/Windows Assets")]
        public static void UpdateWindowsAssets()
        {
            if (!WindowsSettings.Instance.UpdateAssets() && Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        [MenuItem("Robbi/Assets/Build/iOS Assets")]
        public static void BuildiOSAssets()
        {
            iOSSettings.Instance.BuildAssets();
        }

        [MenuItem("Robbi/Assets/Update/iOS Assets")]
        public static void UpdateiOSAssets()
        {
            if (iOSSettings.Instance.UpdateAssets() && Application.isBatchMode)
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

        public static void PreBuildSteps()
        {
            Debug.Log("Beginning Pre Build steps");

            PrepareAssets();
            SetAddressableAssetSettings();
            SetProfileId("AWS");

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

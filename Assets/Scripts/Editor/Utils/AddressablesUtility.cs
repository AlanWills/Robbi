﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace RobbiEditor.Utils
{
    public static class AddressablesUtility
    {
        public static void SetAddressableGroup(this Object o, string group)
        {
            AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(o, out string guid, out long localID);
            AddressableAssetEntry entry = aaSettings.CreateOrMoveEntry(guid, aaSettings.FindGroup(group));
            entry.labels.Add(group);
        }

        public static void SetAddressableGroup(string path, string group)
        {
            string guid = AssetDatabase.AssetPathToGUID(path);
            if (!string.IsNullOrEmpty(guid))
            {
                AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
                AddressableAssetEntry entry = aaSettings.CreateOrMoveEntry(guid, aaSettings.FindGroup(group));
                entry.labels.Add(group);
            }
        }

        public static void SetAddressableAddress(this Object o, string address)
        {
            AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
            AssetDatabase.TryGetGUIDAndLocalFileIdentifier(o, out string guid, out long localID);
            AddressableAssetEntry entry = aaSettings.FindAssetEntry(guid);
            entry.address = address.Replace('\\', '/');
        }

        public static void SetAddressableAddress(string path, string address)
        {
            string guid = AssetDatabase.AssetPathToGUID(path);
            if (!string.IsNullOrEmpty(guid))
            {
                AddressableAssetSettings aaSettings = AddressableAssetSettingsDefaultObject.Settings;
                AddressableAssetEntry entry = aaSettings.FindAssetEntry(guid);
                entry.address = address.Replace('\\', '/');
            }
        }
    }
}

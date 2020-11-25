using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace RobbiEditor.Platform
{
    [CreateAssetMenu(fileName = "AndroidSettings", menuName = "Robbi/Platform/Android Settings")]
    public class AndroidSettings : PlatformSettings
    {
        #region Properties and Fields

        private const string PATH = "Assets/Platform/AndroidSettings.asset";

        private static AndroidSettings instance;
        public static AndroidSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = AssetDatabase.LoadAssetAtPath<AndroidSettings>(PATH);
                }

                return instance;
            }
        }

        #endregion

        protected override void ApplyImpl()
        {
            Version version = ParseVersion(Version);
            PlayerSettings.Android.bundleVersionCode = version.Major * 10000 + version.Minor * 100 + version.Build;
            Debug.LogFormat("Android version is now: {0}", version.ToString());
        }
    }
}

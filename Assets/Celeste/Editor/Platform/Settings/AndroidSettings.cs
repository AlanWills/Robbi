using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace CelesteEditor.Platform
{
    [CreateAssetMenu(fileName = "AndroidSettings", menuName = "Celeste/Platform/Android Settings")]
    public class AndroidSettings : PlatformSettings
    {
        #region Properties and Fields

        private const string DEBUG_PATH = "Assets/Platform/Android/Debug.asset";
        private const string RELEASE_PATH = "Assets/Platform/Android/Release.asset";

        private static AndroidSettings debug;
        public static AndroidSettings Debug
        {
            get
            {
                if (debug == null)
                {
                    debug = AssetDatabase.LoadAssetAtPath<AndroidSettings>(DEBUG_PATH);
                }

                return debug;
            }
        }

        private static AndroidSettings release;
        public static AndroidSettings Release
        {
            get
            {
                if (release == null)
                {
                    release = AssetDatabase.LoadAssetAtPath<AndroidSettings>(RELEASE_PATH);
                }

                return release;
            }
        }

        #endregion

        protected override void ApplyImpl()
        {
            Version version = ParseVersion(Version);
            PlayerSettings.Android.bundleVersionCode = version.Major * 10000 + version.Minor * 100 + version.Build;
            UnityEngine.Debug.LogFormat("Android version is now: {0}", version.ToString());
        }
    }
}

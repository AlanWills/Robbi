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
        private const string RELEASE_APK_PATH = "Assets/Platform/Android/ReleaseApk.asset";
        private const string RELEASE_BUNDLE_PATH = "Assets/Platform/Android/ReleaseBundle.asset";

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

        private static AndroidSettings releaseApk;
        public static AndroidSettings ReleaseApk
        {
            get
            {
                if (releaseApk == null)
                {
                    releaseApk = AssetDatabase.LoadAssetAtPath<AndroidSettings>(RELEASE_APK_PATH);
                }

                return releaseApk;
            }
        }

        private static AndroidSettings releaseBundle;
        public static AndroidSettings ReleaseBundle
        {
            get
            {
                if (releaseBundle == null)
                {
                    releaseBundle = AssetDatabase.LoadAssetAtPath<AndroidSettings>(RELEASE_BUNDLE_PATH);
                }

                return releaseBundle;
            }
        }

        [SerializeField]
        private bool buildAppBundle;
        private bool BuildAppBundle
        {
            get { return buildAppBundle; }
        }

        [SerializeField]
        private string keystorePassword;
        public string KeystorePassword
        {
            get { return keystorePassword; }
        }

        [SerializeField]
        private string keyAliasName;
        public string KeyAliasName
        {
            get { return keyAliasName; }
        }

        [SerializeField]
        private string keyAliasPassword;
        public string KeyAliasPassword
        {
            get { return keyAliasPassword; }
        }

        #endregion

        protected override void ApplyImpl()
        {
            Version version = ParseVersion(Version);
            PlayerSettings.Android.bundleVersionCode = version.Major * 10000 + version.Minor * 100 + version.Build;
            UnityEngine.Debug.LogFormat("Android version is now: {0}", version.ToString());

            PlayerSettings.Android.keystorePass = KeystorePassword;
            PlayerSettings.Android.keyaliasName = KeyAliasName;
            PlayerSettings.Android.keyaliasPass = KeyAliasPassword;
            EditorUserBuildSettings.buildAppBundle = BuildAppBundle;
        }
    }
}

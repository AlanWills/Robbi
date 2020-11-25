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
    public abstract class PlatformSettings : ScriptableObject
    {
        #region Properties and Fields

        [SerializeField]
        private string version;
        public string Version
        {
            get { return version; }
            protected set
            {
                version = value;
                EditorUtility.SetDirty(this);
            }
        }

        [SerializeField]
        private string buildDirectory;
        public string BuildDirectory
        {
            get { return buildDirectory.Replace("{version}", version); }
        }

        [SerializeField]
        private string outputName;
        public string OutputName
        {
            get { return outputName.Replace("{version}", version); }
        }

        [SerializeField]
        private string addressablesBuildDirectory;
        public string AddressablesBuildDirectory
        {
            get { return addressablesBuildDirectory.Replace("{version}", version); }
        }

        [SerializeField]
        private string addressablesLoadDirectory;
        public string AddressablesLoadDirectory
        {
            get { return addressablesLoadDirectory.Replace("{version}", version); }
        }

        [SerializeField]
        private string addressablesS3UploadBucket;
        public string AddressablesS3UploadBucket
        {
            get { return addressablesS3UploadBucket.Replace("{version}", version); }
        }

        [SerializeField]
        private BuildTarget buildTarget;
        public BuildTarget BuildTarget
        {
            get { return buildTarget; }
        }

        [SerializeField]
        private BuildTargetGroup buildTargetGroup;
        public BuildTargetGroup BuildTargetGroup
        {
            get { return buildTargetGroup; }
        }

        #endregion

        #region Platform Setup Methods

        public void BumpVersion()
        {
            Version version = ParseVersion(Version);
            Version = new Version(version.Major, version.Minor, version.Build + 1).ToString();

            Apply();
        }

        public void Apply()
        {
            ApplyImpl();

            PlayerSettings.bundleVersion = version;
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            settings.OverridePlayerVersion = version.ToString();
            settings.profileSettings.SetValue(settings.activeProfileId, "RemoteBuildPath", AddressablesBuildDirectory);
            settings.profileSettings.SetValue(settings.activeProfileId, "RemoteLoadPath", AddressablesLoadDirectory);

            AssetDatabase.SaveAssets();
        }
        protected abstract void ApplyImpl();

        public void Switch()
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup, BuildTarget);
            Apply();
        }

        #endregion

        #region Version Methods

        protected static Version ParseVersion(int bundleVersionCode)
        {
            int major = bundleVersionCode / 10000;
            int minor = (bundleVersionCode - major * 10000) / 100;
            int patch = bundleVersionCode - major * 10000 - minor * 100;

            return new Version(major, minor, patch);
        }

        protected static Version ParseVersion(string bundleString)
        {
            return new Version(bundleString);
        }

        #endregion
    }
}

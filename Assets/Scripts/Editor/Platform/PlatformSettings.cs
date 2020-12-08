using Robbi.Constants;
using Robbi.Options;
using Robbi.Parameters;
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
using UnityEditor.Build.Reporting;
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
        private string playerOverrideVersion;
        public string PlayerOverrideVersion
        {
            get { return Resolve(playerOverrideVersion); }
            protected set
            {
                playerOverrideVersion = value;
                EditorUtility.SetDirty(this);
            }
        }

        [SerializeField]
        private string buildDirectory;
        public string BuildDirectory
        {
            get { return Resolve(buildDirectory); }
        }

        [SerializeField]
        private string outputName;
        public string OutputName
        {
            get { return Resolve(outputName); }
        }

        [SerializeField]
        private string addressablesBuildDirectory;
        public string AddressablesBuildDirectory
        {
            get { return Resolve(addressablesBuildDirectory); }
        }

        [SerializeField]
        private string addressablesLoadDirectory;
        public string AddressablesLoadDirectory
        {
            get { return Resolve(addressablesLoadDirectory); }
        }

        [SerializeField]
        private string addressablesS3UploadBucket;
        public string AddressablesS3UploadBucket
        {
            get { return Resolve(addressablesS3UploadBucket); }
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

        [SerializeField]
        private bool development = true;

        [SerializeField]
        private bool isDebugBuild = false;

        [SerializeField]
        private BuildOptions buildOptions = BuildOptions.Development | BuildOptions.AllowDebugging | BuildOptions.StrictMode;
        public BuildOptions BuildOptions
        {
            get { return buildOptions; }
        }

        #endregion

        #region Platform Setup Methods

        public void BumpVersion()
        {
            Version version = ParseVersion(Version);
            Version = new Version(version.Major, version.Minor, version.Build + 1).ToString();
            Debug.LogFormat("New Version is {0} for platform {1}", Version, BuildTarget);

            Apply();
        }

        public void Apply()
        {
            BuildSystem.BuildAssets.SetAddressableAssetSettings();
            File.WriteAllText("Assets/Resources/" + DebugConstants.IS_DEBUG_BUILD_FILE, isDebugBuild ? "1" : "0");

            ApplyImpl();

            EditorUserBuildSettings.development = development;

            PlayerSettings.bundleVersion = version;
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
            settings.OverridePlayerVersion = PlayerOverrideVersion;
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

        #region Building

        public void BuildPlayer()
        {
            Switch();

            string buildDirectory = BuildDirectory;
            string outputName = OutputName;

            Debug.LogFormat("Build Directory: {0}", buildDirectory);
            Debug.LogFormat("Output Name: {0}", outputName);

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.options = BuildOptions;
            buildPlayerOptions.locationPathName = Path.Combine(buildDirectory, outputName);
            buildPlayerOptions.scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            buildPlayerOptions.target = BuildTarget;
            buildPlayerOptions.targetGroup = BuildTargetGroup;

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            bool success = buildReport != null && buildReport.summary.result == BuildResult.Succeeded;

            if (success)
            {
                File.WriteAllText(Path.Combine(buildDirectory, "BUILD_LOCATION.txt"), buildPlayerOptions.locationPathName);
                BumpVersion();
            }
            else if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
        }

        public void BuildAssets()
        {
            BuildSystem.BuildAssets.PreBuildSteps();
            Switch();

            Debug.Log("Beginning to build content");

            AddressableAssetSettings.BuildPlayerContent();

            StringBuilder locationInfo = new StringBuilder();
            locationInfo.AppendFormat("ASSETS_SOURCE={0}/*", AddressablesBuildDirectory);
            locationInfo.AppendLine();
            locationInfo.AppendFormat("ASSETS_DESTINATION={0}", AddressablesS3UploadBucket);
            File.WriteAllText(Path.Combine(new DirectoryInfo(AddressablesBuildDirectory).Parent.FullName, "ASSETS_ENV_VARS.txt"), locationInfo.ToString());

            Debug.Log("Finished building content");
        }

        public bool UpdateAssets()
        {
            BuildSystem.BuildAssets.PreBuildSteps();
            Switch();

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

        private string Resolve(string stringWithPossibleVersionCodes)
        {
            Version _version = ParseVersion(version);

            return stringWithPossibleVersionCodes.
                    Replace("{version}", version).
                    Replace("{major}", _version.Major.ToString()).
                    Replace("{minor}", _version.Minor.ToString());
        }

        #endregion
    }
}

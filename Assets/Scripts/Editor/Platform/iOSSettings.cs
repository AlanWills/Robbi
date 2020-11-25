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
    [CreateAssetMenu(fileName = "iOSSettings", menuName = "Robbi/Platform/iOS Settings")]
    public class iOSSettings : PlatformSettings
    {
        #region Properties and Fields

        private const string PATH = "Assets/Platform/iOSSettings.asset";

        private static iOSSettings instance;
        public static iOSSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = AssetDatabase.LoadAssetAtPath<iOSSettings>(PATH);
                }

                return instance;
            }
        }

        #endregion

        protected override void ApplyImpl()
        {
            PlayerSettings.iOS.buildNumber = Version;
            Debug.LogFormat("iOS version is now: {0}", PlayerSettings.iOS.buildNumber);
        }
    }
}

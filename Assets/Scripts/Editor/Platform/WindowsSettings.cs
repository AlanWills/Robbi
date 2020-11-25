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
    [CreateAssetMenu(fileName = "WindowsSettings", menuName = "Robbi/Platform/Windows Settings")]
    public class WindowsSettings : PlatformSettings
    {
        #region Properties and Fields

        private const string PATH = "Assets/Platform/WindowsSettings.asset";

        private static WindowsSettings instance;
        public static WindowsSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = AssetDatabase.LoadAssetAtPath<WindowsSettings>(PATH);
                }

                return instance;
            }
        }

        #endregion

        protected override void ApplyImpl()
        {
            // Bump windows version number - yes we're using the mac version number, but windows doesn't have a unique one apparently
            PlayerSettings.macOS.buildNumber = Version;
            Debug.LogFormat("Windows version is now: {0}", PlayerSettings.macOS.buildNumber);
        }
    }
}

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

        private const string DEBUG_PATH = "Assets/Platform/Windows/Debug.asset";
        private const string RELEASE_PATH = "Assets/Platform/Windows/Release.asset";

        private static WindowsSettings debug;
        public static WindowsSettings Debug
        {
            get
            {
                if (debug == null)
                {
                    debug = AssetDatabase.LoadAssetAtPath<WindowsSettings>(DEBUG_PATH);
                }

                return debug;
            }
        }

        private static WindowsSettings release;
        public static WindowsSettings Release
        {
            get
            {
                if (release == null)
                {
                    release = AssetDatabase.LoadAssetAtPath<WindowsSettings>(RELEASE_PATH);
                }

                return release;
            }
        }

        #endregion

        protected override void ApplyImpl()
        {
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget;
        }
    }
}

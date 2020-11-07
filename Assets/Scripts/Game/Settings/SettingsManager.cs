using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Settings
{
    [CreateAssetMenu(menuName = "Robbi/Settings/Settings Manager")]
    public class SettingsManager : PersistentManager<SettingsManager>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Settings/SettingsManager.asset";

        public float MinZoom
        {
            get
            {
#if UNITY_ANDROID || UNITY_IOS
                return mobileMinZoom;
#else
                return desktopMinZoom;
#endif
            }
            set
            {
#if UNITY_ANDROID || UNITY_IOS
                mobileMinZoom = value;
#else
                desktopMinZoom = value;
#endif
            }
        }

        public float MaxZoom
        {
            get
            {
#if UNITY_ANDROID || UNITY_IOS
                return mobileMaxZoom;
#else
                return desktopMaxZoom;
#endif
            }
            set
            {
#if UNITY_ANDROID || UNITY_IOS
                mobileMaxZoom = value;
#else
                desktopMaxZoom = value;
#endif
            }
        }

        public float ZoomSpeed
        {
            get
            {
#if UNITY_ANDROID || UNITY_IOS
                return mobileZoomSpeed;
#else
                return desktopZoomSpeed;
#endif
            }
            set
            {
#if UNITY_ANDROID || UNITY_IOS
                mobileZoomSpeed = value;
#else
                desktopZoomSpeed = value;
#endif
            }
        }

        public float DragSpeed
        {
            get
            {
#if UNITY_ANDROID || UNITY_IOS
                return mobileDragSpeed;
#else
                return desktopDragSpeed;
#endif
            }
            set
            {
#if UNITY_ANDROID || UNITY_IOS
                mobileDragSpeed = value;
#else
                desktopDragSpeed = value;
#endif
            }
        }

        public bool MusicEnabled
        {
            get { return musicEnabled.value; }
            set { musicEnabled.value = value; }
        }

        public bool SfxEnabled
        {
            get { return sfxEnabled.value; }
            set { sfxEnabled.value = value; }
        }

        #region Common Settings

        [SerializeField]
        private BoolValue musicEnabled;

        [SerializeField]
        private BoolValue sfxEnabled;

        #endregion

        #region Mobile Specific Settings

        [SerializeField]
        private float mobileMinZoom = 8;

        [SerializeField]
        private float mobileMaxZoom = 20;

        [SerializeField]
        private float mobileZoomSpeed = 0.02f;

        [SerializeField]
        private float mobileDragSpeed = 0.05f;

        #endregion

        #region Desktop Specific Settings

        [SerializeField]
        private float desktopMinZoom = 2;

        [SerializeField]
        private float desktopMaxZoom = 20;

        [SerializeField]
        private float desktopZoomSpeed = 1;

        [SerializeField]
        private float desktopDragSpeed = 1;

        #endregion

        #endregion

        private SettingsManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS);
        }

        #endregion
    }
}

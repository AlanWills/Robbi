using Robbi.Debugging.Logging;
using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Options
{
    [CreateAssetMenu(menuName = "Robbi/Options/Options Manager")]
    public class OptionsManager : PersistentManager<OptionsManager>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Options/OptionsManager.asset";
        
        public static string DefaultSavePath
        {
            get { return Path.Combine(Application.persistentDataPath, "OptionsManager.json"); }
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

        public float DefaultMovementSpeed
        {
            get { return defaultMovementSpeed.value; }
            set { defaultMovementSpeed.value = value; }
        }

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

        #region Common Settings

        [SerializeField]
        private BoolValue musicEnabled;

        [SerializeField]
        private BoolValue sfxEnabled;

        [SerializeField]
        private FloatValue defaultMovementSpeed;

        #endregion

        #region Mobile Specific Settings

        [SerializeField]
        private float mobileMinZoom = 0.75f;

        [SerializeField]
        private float mobileMaxZoom = 8;

        [SerializeField]
        private float mobileZoomSpeed = 0.02f;

        [SerializeField]
        private float mobileDragSpeed = 0.05f;

        #endregion

        #region Desktop Specific Settings

        [SerializeField]
        private float desktopMinZoom = 0.75f;

        [SerializeField]
        private float desktopMaxZoom = 2;

        [SerializeField]
        private float desktopZoomSpeed = 1;

        [SerializeField]
        private float desktopDragSpeed = 1;

        #endregion

        #endregion

        private OptionsManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS, DefaultSavePath);
        }

        public void Save()
        {
            Save(DefaultSavePath);
        }

        protected override string Serialize()
        {
            return JsonUtility.ToJson(new OptionsManagerDTO(this));
        }

        protected override void Deserialize(string fileContents)
        {
            OptionsManagerDTO optionsManagerDTO = JsonUtility.FromJson<OptionsManagerDTO>(fileContents);

            MusicEnabled = optionsManagerDTO.musicEnabled;
            SfxEnabled = optionsManagerDTO.sfxEnabled;
            DefaultMovementSpeed = optionsManagerDTO.defaultMovementSpeed;

            HudLogger.LogInfoFormat("Music Enabled: {0}", MusicEnabled);
            HudLogger.LogInfoFormat("Sfx Enabled: {0}", SfxEnabled);
            HudLogger.LogInfoFormat("Default Movement Speed: {0}", DefaultMovementSpeed);
        }

        #endregion
    }

    [Serializable]
    public class OptionsManagerDTO
    {
        public bool musicEnabled = true;
        public bool sfxEnabled = true;
        public float defaultMovementSpeed = 4;

        public OptionsManagerDTO() { }

        public OptionsManagerDTO(OptionsManager optionsManager)
        {
            musicEnabled = optionsManager.MusicEnabled;
            sfxEnabled = optionsManager.SfxEnabled;
            defaultMovementSpeed = optionsManager.DefaultMovementSpeed;
        }
    }
}
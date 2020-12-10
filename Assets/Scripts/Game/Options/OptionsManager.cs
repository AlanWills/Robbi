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
            get { return musicEnabled.Value; }
            set { musicEnabled.Value = value; }
        }

        public bool SfxEnabled
        {
            get { return sfxEnabled.Value; }
            set { sfxEnabled.Value = value; }
        }

        public bool BatterySaver
        {
            get { return batterySaver.Value; }
            set 
            {
                if (value != batterySaver.Value)
                {
                    batterySaver.Value = value;
                    SyncFrameRate();
                }
            }
        }

        public float DefaultMovementSpeed
        {
            get { return defaultMovementSpeed.Value; }
            set { defaultMovementSpeed.Value = value; }
        }

        [SerializeField]
        private BoolValue musicEnabled;

        [SerializeField]
        private BoolValue sfxEnabled;

        [SerializeField]
        private BoolValue batterySaver;

        [SerializeField]
        private FloatValue defaultMovementSpeed;

        [NonSerialized]
        public float MinZoom = 1;

        [NonSerialized]
        public float MaxZoom = 1;

        [NonSerialized]
        public float ZoomSpeed = 0;

        [NonSerialized]
        public float DragSpeed = 0;

        #endregion

        private OptionsManager() { }

        #region Save/Load Methods

        public static AsyncOperationHandle Load()
        {
            return Load(ADDRESS, DefaultSavePath);
        }

        public static void Reset()
        {
            if (File.Exists(DefaultSavePath))
            {
                File.Delete(DefaultSavePath);
            }
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
            BatterySaver = optionsManagerDTO.batterySaver;
            DefaultMovementSpeed = optionsManagerDTO.defaultMovementSpeed;
            MinZoom = optionsManagerDTO.minZoom;
            MaxZoom = optionsManagerDTO.maxZoom;
            ZoomSpeed = optionsManagerDTO.zoomSpeed;
            DragSpeed = optionsManagerDTO.dragSpeed;

            HudLogger.LogInfoFormat("Music Enabled: {0}", MusicEnabled);
            HudLogger.LogInfoFormat("Sfx Enabled: {0}", SfxEnabled);
            HudLogger.LogInfoFormat("Battery Saver: {0}", BatterySaver);
            HudLogger.LogInfoFormat("Default Movement Speed: {0}", DefaultMovementSpeed);
            HudLogger.LogInfoFormat("Min Speed: {0}", MinZoom);
            HudLogger.LogInfoFormat("Max Speed: {0}", MaxZoom);
            HudLogger.LogInfoFormat("Zoom Speed: {0}", ZoomSpeed);
            HudLogger.LogInfoFormat("Drag Speed: {0}", DragSpeed);
        }

        #endregion

        #region Utility Methods

        // Need this separate to be able to call this from node graph
        public void SyncFrameRate()
        {
            Application.targetFrameRate = BatterySaver ? 30 : 60;
        }

        #endregion
    }

    [Serializable]
    public class OptionsManagerDTO
    {
        public bool musicEnabled = true;
        public bool sfxEnabled = true;
        public bool batterySaver = false;
        public float defaultMovementSpeed = 4;

#if UNITY_ANDROID || UNITY_IOS
        public float minZoom = 0.75f;
        public float maxZoom = 8;
        public float zoomSpeed = 0.02f;
        public float dragSpeed = 0.05f;
#elif UNITY_STANDALONE
        public float minZoom = 0.5f;
        public float maxZoom = 1.75f;
        public float zoomSpeed = 0.85f;
        public float dragSpeed = 0.1f;
#elif UNITY_WEBGL
        public float minZoom = 0.5f;
        public float maxZoom = 1.75f;
        public float zoomSpeed = 0.85f;
        public float dragSpeed = 0.1f;
#endif
        public OptionsManagerDTO() { }

        public OptionsManagerDTO(OptionsManager optionsManager)
        {
            musicEnabled = optionsManager.MusicEnabled;
            sfxEnabled = optionsManager.SfxEnabled;
            batterySaver = optionsManager.BatterySaver;
            defaultMovementSpeed = optionsManager.DefaultMovementSpeed;
            minZoom = optionsManager.MinZoom;
            maxZoom = optionsManager.MaxZoom;
            zoomSpeed = optionsManager.ZoomSpeed;
            dragSpeed = optionsManager.DragSpeed;
        }
    }
}
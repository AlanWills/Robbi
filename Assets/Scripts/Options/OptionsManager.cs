using Celeste.Log;
using Celeste.Managers;
using Celeste.Parameters;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Options
{
    [CreateAssetMenu(menuName = "Robbi/Options/Options Manager")]
    public class OptionsManager : PersistentManager<OptionsManager, OptionsManagerDTO>
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
                batterySaver.Value = value;
                SyncBatterySaverOptions();
            }
        }

        public float DefaultMovementSpeed
        {
            get { return defaultMovementSpeed.Value; }
            set { defaultMovementSpeed.Value = value; }
        }

        public float MinZoom
        {
            get { return minZoom.Value; }
            set { minZoom.Value = value; }
        }

        public float MaxZoom
        {
            get { return maxZoom.Value; }
            set { maxZoom.Value = value; }
        }

        public float ZoomSpeed
        {
            get { return zoomSpeed.Value; }
            set { zoomSpeed.Value = value; }
        }

        public float DragSpeed
        {
            get { return dragSpeed.Value; }
            set { dragSpeed.Value = value; }
        }

        [SerializeField]
        private BoolValue musicEnabled;

        [SerializeField]
        private BoolValue sfxEnabled;

        [SerializeField]
        private BoolValue batterySaver;

        [SerializeField]
        private FloatValue defaultMovementSpeed;

        [SerializeField]
        private FloatValue minZoom;

        [SerializeField]
        private FloatValue maxZoom;

        [SerializeField]
        private FloatValue zoomSpeed;

        [SerializeField]
        private FloatValue dragSpeed;

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

        protected override void Deserialize(OptionsManagerDTO optionsManagerDTO)
        {
            MusicEnabled = optionsManagerDTO.musicEnabled;
            SfxEnabled = optionsManagerDTO.sfxEnabled;
            BatterySaver = optionsManagerDTO.batterySaver;
            DefaultMovementSpeed = optionsManagerDTO.defaultMovementSpeed;
            MinZoom = optionsManagerDTO.minZoom;
            MaxZoom = optionsManagerDTO.maxZoom;
            ZoomSpeed = optionsManagerDTO.zoomSpeed;
            DragSpeed = optionsManagerDTO.dragSpeed;

            HudLog.LogInfoFormat("Music Enabled: {0}", MusicEnabled);
            HudLog.LogInfoFormat("Sfx Enabled: {0}", SfxEnabled);
            HudLog.LogInfoFormat("Battery Saver: {0}", BatterySaver);
            HudLog.LogInfoFormat("Default Movement Speed: {0}", DefaultMovementSpeed);
            HudLog.LogInfoFormat("Min Speed: {0}", MinZoom);
            HudLog.LogInfoFormat("Max Speed: {0}", MaxZoom);
            HudLog.LogInfoFormat("Zoom Speed: {0}", ZoomSpeed);
            HudLog.LogInfoFormat("Drag Speed: {0}", DragSpeed);
        }

        protected override void SetDefaultValues()
        {
#if UNITY_ANDROID || UNITY_IOS
            MinZoom = 0.75f;
            MaxZoom = 1.5f;
            ZoomSpeed = 0.02f;
            DragSpeed = 0.05f;
#elif UNITY_STANDALONE
            MinZoom = 0.5f;
            MaxZoom = 1.5f;
            ZoomSpeed = 0.85f;
            DragSpeed = 0.1f;
#elif UNITY_WEBGL
            MinZoom = 0.5f;
            MaxZoom = 1.5f;
            ZoomSpeed = 0.85f;
            DragSpeed = 0.1f;
#endif

            SyncBatterySaverOptions();
        }

        #endregion

        #region Utility Methods

        // Need this separate to be able to call this from node graph
        public void SyncBatterySaverOptions()
        {
            QualitySettings.vSyncCount = BatterySaver ? 0 : 1;
            UnityEngine.Application.targetFrameRate = BatterySaver ? 30 : 60;
        }

        #endregion
    }

    [Serializable]
    public struct OptionsManagerDTO
    {
        public bool musicEnabled;
        public bool sfxEnabled;
        public bool batterySaver;
        public float defaultMovementSpeed;
        public float minZoom;
        public float maxZoom;
        public float zoomSpeed;
        public float dragSpeed;
      
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
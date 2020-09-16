using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Settings
{
    [Serializable]
    public struct GameSettingsDTO
    {
        public float minZoom;
        public float maxZoom;
        public float zoomSpeed;
        public float dragSpeed;

        public GameSettingsDTO(GameSettings gameSettings)
        {
            minZoom = gameSettings.MinZoom;
            maxZoom = gameSettings.MaxZoom;
            zoomSpeed = gameSettings.ZoomSpeed;
            dragSpeed = gameSettings.DragSpeed;
        }
    }

    public class GameSettings : ScriptableObject
    {
        #region Properties and Fields

        private static string DEFAULT_FILE_PATH = "";

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

        #endregion

        private GameSettings() { }

        #region Unity Methods

        private void OnEnable()
        {
            DEFAULT_FILE_PATH = Path.Combine(Application.persistentDataPath, "GameSettings.json");
        }

        #endregion

        #region Save/Load Methods

    public static GameSettings Load()
        {
            return Load(DEFAULT_FILE_PATH);
        }

        public static GameSettings Load(string fileName)
        {
            GameSettings gameSettings = ScriptableObject.CreateInstance<GameSettings>();

            if (File.Exists(fileName))
            {
                GameSettingsDTO gameSettingsDTO = JsonUtility.FromJson<GameSettingsDTO>(File.ReadAllText(fileName));
                gameSettings.MinZoom = gameSettingsDTO.minZoom;
                gameSettings.MaxZoom = gameSettingsDTO.maxZoom;
                gameSettings.ZoomSpeed = gameSettingsDTO.zoomSpeed;
                gameSettings.DragSpeed = gameSettingsDTO.dragSpeed;
            }

            return gameSettings;
        }

        public void Save()
        {
            Save(DEFAULT_FILE_PATH);
        }

        public void Save(string fileName)
        {
            GameSettingsDTO gameSettingsDTO = new GameSettingsDTO(this);
            string json = JsonUtility.ToJson(gameSettingsDTO);
            File.WriteAllText(fileName, json);
        }

        #endregion
    }
}

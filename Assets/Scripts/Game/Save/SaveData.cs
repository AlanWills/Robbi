using Robbi.Debugging.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Save
{
    [Serializable]
    public class SaveData
    {
        #region Properties and Fields

        private static string DEFAULT_FILE_PATH
        {
            get { return Path.Combine(Application.persistentDataPath, "SaveData.json"); }
        }

        public uint currentLevel = 0;
        public bool musicEnabled = true;
        public bool sfxEnabled = true;
        public float defaultMovementSpeed = 4;

        #endregion

        #region Save/Load

        public static SaveData Load()
        {
            return Load(DEFAULT_FILE_PATH);
        }

        public static SaveData Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
                Debug.LogFormat("SaveData loaded {0}", saveData != null ? "correctly" : "incorrectly");
                return saveData;
            }
            else
            {
                Debug.LogWarningFormat("Could not find Save Data file {0} so using default values", filePath);
                return new SaveData();
            }
        }

        public void Save()
        {
            Save(DEFAULT_FILE_PATH);
        }

        public void Save(string filePath)
        {
            string json = JsonUtility.ToJson(this);
            File.WriteAllText(filePath, json);

            HudLogger.LogInfo("Save Data correctly");
            HudLogger.LogInfo(string.Format("Current Level: {0}", currentLevel));
            HudLogger.LogInfo(string.Format("Music Enabled: {0}", musicEnabled));
            HudLogger.LogInfo(string.Format("Sfx Enabled: {0}", sfxEnabled));
            HudLogger.LogInfo(string.Format("Default Movement Speed: {0}", defaultMovementSpeed));
        }

        #endregion
    }
}

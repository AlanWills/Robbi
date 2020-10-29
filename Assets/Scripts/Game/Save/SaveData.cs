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

        public uint currentLevel;

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
                return JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
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

            HudLogger.LogInfo(string.Format("Save Data saved with current level {0}", currentLevel));
        }

        #endregion
    }
}

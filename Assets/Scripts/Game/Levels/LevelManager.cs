using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels
{
    [Serializable]
    public struct LevelManagerDTO
    {
        public uint currentLevel;

        public LevelManagerDTO(LevelManager levelManager)
        {
            currentLevel = levelManager.CurrentLevelIndex;
        }
    }

    public class LevelManager : ScriptableObject
    {
        #region Properties and Fields

        private static string DEFAULT_FILE_PATH = "LevelManagerData.json";

        public uint CurrentLevelIndex { get; set; }

        #endregion

        private LevelManager() { }

        #region Unity Methods

        private void OnEnable()
        {
            DEFAULT_FILE_PATH = Path.Combine(Application.persistentDataPath, "LevelManagerData.json");
        }

        #endregion

        #region Save/Load Methods

        public static LevelManager Load()
        {
            return Load(DEFAULT_FILE_PATH);
        }

        public static LevelManager Load(string fileName)
        {
            LevelManager levelManager = ScriptableObject.CreateInstance<LevelManager>();

            if (File.Exists(fileName))
            {
                LevelManagerDTO levelManagerDTO = JsonUtility.FromJson<LevelManagerDTO>(File.ReadAllText(fileName));
                levelManager.CurrentLevelIndex = levelManagerDTO.currentLevel;
            }

            return levelManager;
        }

        public void Save()
        {
            Save(DEFAULT_FILE_PATH);
        }

        public void Save(string fileName)
        {
            LevelManagerDTO levelManagerDTO = new LevelManagerDTO(this);
            string json = JsonUtility.ToJson(levelManagerDTO);
            File.WriteAllText(fileName, json);
        }

        #endregion
    }
}

﻿using Celeste.Log;
using Celeste.Managers;
using Celeste.Parameters;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Robbi.Levels
{
    [CreateAssetMenu(menuName = "Robbi/Levels/Level Manager")]
    public class LevelManager : PersistentManager<LevelManager, LevelManagerDTO>
    {
        #region Properties and Fields

        private const string ADDRESS = "Assets/Levels/LevelManager.asset";

        public static string DefaultSavePath
        {
            get { return Path.Combine(UnityEngine.Application.persistentDataPath, "LevelManager.json"); }
        }

        public uint CurrentLevel
        { 
            get { return currentLevel.Value; }
            set { currentLevel.Value = value; }
        }

        public uint CurrentLevel_DefaultValue
        {
            get { return currentLevel.DefaultValue; }
            set { currentLevel.DefaultValue = value; }
        }

        public uint LatestUnlockedLevel
        {
            get { return latestUnlockedLevel.Value; }
            set { latestUnlockedLevel.Value = value; }
        }

        public uint LatestAvailableLevel
        {
            get { return latestAvailableLevel.Value; }
            set { latestAvailableLevel.Value = value; }
        }

        public uint LatestAvailableLevel_DefaultValue
        {
            get { return latestAvailableLevel.DefaultValue; }
            set { latestAvailableLevel.DefaultValue = value; }
        }

        [SerializeField]
        private UIntValue currentLevel;

        [SerializeField]
        private UIntValue latestUnlockedLevel;

        [SerializeField]
        private UIntValue latestAvailableLevel;

#endregion

        private LevelManager() { }

#region Save/Load Methods

#region Editor Only

#if UNITY_EDITOR

        public static LevelManager EditorOnly_Load()
        {
            return EditorOnly_Load(ADDRESS);
        }

#endif

#endregion

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
            return JsonUtility.ToJson(new LevelManagerDTO(this));
        }

        protected override void Deserialize(LevelManagerDTO levelManagerDTO)
        {
            LatestUnlockedLevel = levelManagerDTO.latestUnlockedLevelIndex;

            HudLog.LogInfoFormat("Latest Unlocked Level Index: {0}", LatestUnlockedLevel);
            HudLog.LogInfoFormat("Latest Available Level Index: {0}", LatestAvailableLevel);
        }

        #endregion
    }

    [Serializable]
    public class LevelManagerDTO
    {
        public uint latestUnlockedLevelIndex = 0;

        public LevelManagerDTO() { }

        public LevelManagerDTO(LevelManager levelManager)
        {
            latestUnlockedLevelIndex = levelManager.LatestUnlockedLevel;
        }
    }
}
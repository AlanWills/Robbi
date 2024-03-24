using Celeste.Log;
using Celeste.Persistence;
using System;
using UnityEngine;

namespace Robbi.Levels
{
    [AddComponentMenu("Robbi/Levels/Level Manager")]
    public class LevelManager : PersistentSceneManager<LevelManager, LevelRecordDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "LevelManager.dat";

        protected override string FileName => FILE_NAME;

        [SerializeField] private LevelRecord levelRecord;

        #endregion

        #region Save/Load Methods

        protected override LevelRecordDTO Serialize()
        {
            return new LevelRecordDTO(levelRecord);
        }

        protected override void Deserialize(LevelRecordDTO levelManagerDTO)
        {
            levelRecord.LatestUnlockedLevel = levelManagerDTO.latestUnlockedLevelIndex;

            HudLog.LogInfo($"Latest Unlocked Level Index: {levelRecord.LatestUnlockedLevel}");
            HudLog.LogInfo($"Latest Available Level Index: {levelRecord.LatestAvailableLevel}");
        }

        protected override void SetDefaultValues() { }

        #endregion
    }

    [Serializable]
    public class LevelRecordDTO
    {
        public uint latestUnlockedLevelIndex;

        public LevelRecordDTO(LevelRecord levelRecord)
        {
            latestUnlockedLevelIndex = levelRecord.LatestUnlockedLevel;
        }
    }
}

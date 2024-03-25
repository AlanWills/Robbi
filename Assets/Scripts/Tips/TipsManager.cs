using Celeste.Persistence;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Celeste.Tips
{
    [AddComponentMenu("Robbi/Tips/Tips Manager")]
    public class TipsManager : PersistentSceneManager<TipsManager, TipsManagerDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "Tips.dat";
        protected override string FileName => FILE_NAME;

        [SerializeField] private TipsRecord tipsRecord;

        #endregion

        #region Save/Load Methods

        protected override TipsManagerDTO Serialize()
        {
            return new TipsManagerDTO(tipsRecord);
        }

        protected override void Deserialize(TipsManagerDTO tipsManagerDTO)
        {
            tipsRecord.Initialize(tipsManagerDTO.unseenIndexes, tipsManagerDTO.seenIndexes);
        }

        protected override void SetDefaultValues()
        {
            tipsRecord.Initialize();
        }

        #endregion
    }

    [System.Serializable]
    public class TipsManagerDTO
    {
        public List<uint> unseenIndexes;
        public List<uint> seenIndexes;

        public TipsManagerDTO(TipsRecord tipsRecord)
        {
            unseenIndexes = new List<uint>(tipsRecord.UnseenIndexes);
            seenIndexes = new List<uint>(tipsRecord.SeenIndexes);
            UnityEngine.Debug.Assert(tipsRecord.AllTips.Count == (unseenIndexes.Count + seenIndexes.Count), 
                $"Tip index mismatch.  All: {tipsRecord.AllTips.Count}.  Unseen: {unseenIndexes.Count}.  Seen: {seenIndexes.Count}");
        }
    }
}

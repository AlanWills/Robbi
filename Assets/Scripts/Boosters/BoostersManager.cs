using Celeste.Persistence;
using System;
using UnityEngine;

namespace Robbi.Boosters
{
    [CreateAssetMenu(menuName = "Robbi/Boosters/Boosters Manager")]
    public class BoostersManager : PersistentSceneManager<BoostersManager, BoostersRecordDTO>
    {
        #region Properties and Fields

        public const string FILE_NAME = "BoostersManager.dat";

        protected override string FileName => FILE_NAME;

        [SerializeField] private BoostersRecord boostersRecord;

        #endregion

        #region Save/Load Methods

        protected override BoostersRecordDTO Serialize()
        {
            return new BoostersRecordDTO(boostersRecord);
        }

        protected override void Deserialize(BoostersRecordDTO optionsManagerDTO)
        {
            boostersRecord.NumWaypointBoosters = optionsManagerDTO.numWaypointBoosters;
            boostersRecord.NumDoorToggleBoosters = optionsManagerDTO.numDoorToggleBoosters;
            boostersRecord.NumInteractBoosters = optionsManagerDTO.numInteractBoosters;
        }

        protected override void SetDefaultValues() { }

        #endregion
    }

    [Serializable]
    public class BoostersRecordDTO
    {
        public uint numWaypointBoosters;
        public uint numDoorToggleBoosters;
        public uint numInteractBoosters;

        public BoostersRecordDTO(BoostersRecord boostersRecord)
        {
            numWaypointBoosters = boostersRecord.NumWaypointBoosters;
            numDoorToggleBoosters = boostersRecord.NumDoorToggleBoosters;
            numInteractBoosters = boostersRecord.NumInteractBoosters;
        }
    }
}

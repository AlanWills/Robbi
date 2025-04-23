using System;
using Celeste.Persistence;

namespace Robbi.Boosters
{
    [Serializable]
    public class BoostersRecordDTO : VersionedDTO
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
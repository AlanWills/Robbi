using System;

namespace Robbi.Boosters
{
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
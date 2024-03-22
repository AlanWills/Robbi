using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Boosters
{
    [CreateAssetMenu(fileName = nameof(BoostersRecord), menuName = "Robbi/Boosters/Boosters Record")]
    public class BoostersRecord : ScriptableObject
    {
        #region Properties and Fields

        public uint NumWaypointBoosters
        {
            get { return numWaypointBoosters.Value; }
            set 
            {
                if (NumWaypointBoosters != value)
                {
                    numWaypointBoosters.Value = value;
                    onChanged.Invoke();
                }
            }
        }

        public uint NumDoorToggleBoosters
        {
            get { return numDoorToggleBoosters.Value; }
            set 
            { 
                if (NumDoorToggleBoosters != value)
                {
                    numDoorToggleBoosters.Value = value; 
                    onChanged.Invoke();
                }
            }
        }

        public uint NumInteractBoosters
        {
            get { return numInteractBoosters.Value; }
            set 
            {
                if (NumInteractBoosters != value)
                {
                    numInteractBoosters.Value = value;
                    onChanged.Invoke();
                }
            }
        }

        [Header("Data")]
        [SerializeField] private UIntValue numWaypointBoosters;
        [SerializeField] private UIntValue waypointBoosterUnlockLevel;
        [SerializeField] private UIntValue numDoorToggleBoosters;
        [SerializeField] private UIntValue doorToggleBoosterUnlockLevel;
        [SerializeField] private UIntValue numInteractBoosters;
        [SerializeField] private UIntValue interactBoosterUnlockLevel;

        [Header("Events")]
        [SerializeField] private Celeste.Events.Event onChanged;

        #endregion
    }
}
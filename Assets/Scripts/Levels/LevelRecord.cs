using Celeste.Parameters;
using UnityEngine;

namespace Robbi.Levels
{
    [CreateAssetMenu(fileName = nameof(LevelRecord), menuName = "Robbi/Levels/Level Record")]
    public class LevelRecord : ScriptableObject
    {
        #region Properties and Fields

        public uint CurrentLevel
        { 
            get => currentLevel.Value;
            set 
            {
                if (value != CurrentLevel)
                {
                    currentLevel.Value = value;
                    onChanged.Invoke();
                }
            }
        }

        public uint LatestUnlockedLevel
        {
            get => latestUnlockedLevel.Value;
            set 
            {
                if (value != LatestUnlockedLevel)
                {
                    latestUnlockedLevel.Value = value;
                    onChanged.Invoke();
                }
            }
        }

        public uint LatestAvailableLevel
        {
            get => latestAvailableLevel.Value;
            set 
            {
                if (value != LatestAvailableLevel)
                {
                    latestAvailableLevel.Value = value;
                    onChanged.Invoke();
                }
            }
        }

        [Header("Data")]
        [SerializeField] private UIntValue currentLevel;
        [SerializeField] private UIntValue latestUnlockedLevel;
        [SerializeField] private UIntValue latestAvailableLevel;

        [Header("Events")]
        [SerializeField] private Celeste.Events.Event onChanged;

        #endregion
    }
}

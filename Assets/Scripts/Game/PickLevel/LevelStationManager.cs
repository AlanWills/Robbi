using dynamicscroll;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Robbi.PickLevel
{
    [AddComponentMenu("Robbi/Pick Level/Level Station Manager")]
    public class LevelStationManager : MonoBehaviour
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField]
        private DynamicScrollRect dynamicScrollRect;

        [SerializeField]
        private GameObject levelStationPrefab;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue latestUnlockedLevel;

        [SerializeField]
        private UIntValue latestAvailableLevel;

        private DynamicScroll<LevelStationData, LevelStation> levelStationScroll = new DynamicScroll<LevelStationData, LevelStation>();
        private List<LevelStationData> levelStationData = new List<LevelStationData>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            uint instantiationCount = Math.Min(latestUnlockedLevel.Value, latestAvailableLevel.Value) + 1;

            for (uint i = 0; i < instantiationCount; ++i)
            {
                levelStationData.Add(new LevelStationData(i, i < latestUnlockedLevel.Value));
            }

            levelStationScroll.Initiate(dynamicScrollRect, levelStationData, 0, levelStationPrefab);
        }
            
        #endregion
    }
}

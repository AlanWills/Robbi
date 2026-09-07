using PolyAndCode.UI;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.PickLevel
{
    [AddComponentMenu("Robbi/Pick Level/Level Station Manager")]
    public class LevelStationManager : MonoBehaviour, IRecyclableScrollRectDataSource
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField] private RecyclableScrollRect scrollRect;

        [Header("Parameters")]
        [SerializeField] private UIntValue latestUnlockedLevel;
        [SerializeField] private UIntValue latestAvailableLevel;

        private readonly List<LevelStationData> levelStationData = new();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            uint latestLevel = Math.Min(latestUnlockedLevel.Value, latestAvailableLevel.Value);
            uint instantiationCount = latestLevel + 1;

            for (uint i = 0; i < instantiationCount; ++i)
            {
                levelStationData.Add(new LevelStationData(i, i < latestUnlockedLevel.Value));
            }

            scrollRect.Initialize(this);
        }

        #endregion

        #region Data Source Methods

        public int GetItemCount()
        {
            return levelStationData.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            LevelStation levelStation = cell as LevelStation;
            levelStation.ConfigureCell(levelStationData[index], index);
        }

        #endregion
    }
}

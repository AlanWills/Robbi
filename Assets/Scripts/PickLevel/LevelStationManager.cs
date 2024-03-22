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

        [SerializeField] private int portraitGridSegments;
        [SerializeField] private int landscapeGridSegments;

        private List<LevelStationData> levelStationData = new List<LevelStationData>();

        #endregion

        #region Unity Methods

        private void Awake()
        {
            //scrollRect.Segments = Screen.width > Screen.height ? landscapeGridSegments : portraitGridSegments;

            uint latestLevel = Math.Min(latestUnlockedLevel.Value, latestAvailableLevel.Value);
            uint instantiationCount = latestLevel + 1;

            for (uint i = 0; i < instantiationCount; ++i)
            {
                levelStationData.Add(new LevelStationData(i, i < latestUnlockedLevel.Value));
            }

            scrollRect.DataSource = this;
        }

        #endregion

        #region Data Source Methods

        public int GetItemCount()
        {
            return levelStationData.Count;
        }

        public void SetCell(ICell cell, int index)
        {
            // Show elements in reverse order so that the latest levels will appear at the top
            uint latestLevel = Math.Min(latestUnlockedLevel.Value, latestAvailableLevel.Value);
            int reverseIndex = (int)latestLevel - index;

            LevelStation levelStation = cell as LevelStation;
            levelStation.ConfigureCell(levelStationData[reverseIndex], reverseIndex);
        }

        #endregion
    }
}

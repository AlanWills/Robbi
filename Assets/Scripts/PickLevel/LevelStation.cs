using PolyAndCode.UI;
using Celeste.Events;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Robbi.PickLevel
{
    [AddComponentMenu("Robbi/Pick Level/Level Station")]
    [RequireComponent(typeof(RectTransform))]
    public class LevelStation : MonoBehaviour, ICell
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI levelIndexText;
        
        [SerializeField]
        private GameObject levelCompleteIcon;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue currentLevel;

        private uint currentIndex;

        #endregion

        #region Level Station Utility Methods

        public void ConfigureCell(LevelStationData levelStationData, int cellIndex)
        {
            currentIndex = (uint)cellIndex;

            name = string.Format("LevelStation{0}", levelStationData.levelIndex);
            levelIndexText.text = levelStationData.levelIndex.ToString();
            levelCompleteIcon.SetActive(levelStationData.isComplete);
        }

        public void SetCurrentLevel()
        {
            currentLevel.Value = currentIndex;
        }

        #endregion
    }
}

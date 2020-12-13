using dynamicscroll;
using Robbi.Events;
using Robbi.Parameters;
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
    public class LevelStation : DynamicScrollObject<LevelStationData>
    {
        #region Properties and Fields

        public override float CurrentHeight { get; set; }
        public override float CurrentWidth { get; set; }

        [Header("UI")]
        private TextMeshProUGUI levelIndexText;
        private GameObject levelCompleteIcon;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue currentLevel;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Rect rect = GetComponent<RectTransform>().rect;
            CurrentHeight = rect.height;
            CurrentWidth = rect.width;

            Transform levelStationButton = transform.Find("PlayLevelButton");
            levelIndexText = levelStationButton.Find("LevelIndexText").GetComponent<TextMeshProUGUI>();
            levelCompleteIcon = levelStationButton.Find("LevelCompleteIcon").gameObject;
        }

        #endregion

        #region Level Station Utility Methods

        public override void UpdateScrollObject(LevelStationData item, int index)
        {
            base.UpdateScrollObject(item, index);

            name = string.Format("LevelStation{0}", item.levelIndex);
            levelIndexText.text = item.levelIndex.ToString();
            levelCompleteIcon.SetActive(item.isComplete);
        }

        public void SetCurrentLevel()
        {
            currentLevel.Value = (uint)CurrentIndex;
        }

        #endregion
    }
}

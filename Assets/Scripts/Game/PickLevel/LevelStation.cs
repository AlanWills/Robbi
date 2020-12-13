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
    public class LevelStation : DynamicScrollObject<LevelStationData>
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
            //this.
        }

        #endregion
    }
}

﻿using Celeste.Tools;
using TMPro;
using UnityEngine;

namespace Celeste.Tips
{
    [AddComponentMenu("Robbi/Tips/Tip Controller")]
    public class TipController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TipsRecord tipsRecord;
        [SerializeField] private TextMeshProUGUI tipText;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            this.TryGet(ref tipText);
        }

        private void OnEnable()
        {
            tipText.text = tipsRecord.RandomTip;
        }

        #endregion
    }
}

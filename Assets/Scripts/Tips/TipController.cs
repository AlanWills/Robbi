using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Celeste.Tips
{
    [AddComponentMenu("Robbi/Tips/Tip Controller")]
    public class TipController : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TextMeshProUGUI tipText;

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (tipText == null)
            {
                tipText = GetComponent<TextMeshProUGUI>();
            }
        }

        private void OnEnable()
        {
            tipText.text = TipsManager.Instance.RandomTip;
            TipsManager.Instance.SaveAsync();
        }

        #endregion
    }
}

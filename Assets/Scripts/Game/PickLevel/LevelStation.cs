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
    public class LevelStation : MonoBehaviour
    {
        #region Properties and Fields

        private uint index;
        public uint Index
        {
            get { return index; }
            set
            {
                index = value;
                levelIndexText.text = index.ToString();
            }
        }

        public bool Complete
        {
            set { levelCompleteIcon.SetActive(value); }
        }

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI levelIndexText;

        [SerializeField]
        private GameObject levelCompleteIcon;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue currentLevel;

        #endregion

        #region Play Level

        public void SetCurrentLevel()
        {
            currentLevel.Value = Index;
        }

        #endregion
    }
}

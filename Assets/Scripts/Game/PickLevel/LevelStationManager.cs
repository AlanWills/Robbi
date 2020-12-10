using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.PickLevel
{
    [AddComponentMenu("Robbi/Pick Level/Level Station Manager")]
    public class LevelStationManager : MonoBehaviour
    {
        #region Properties and Fields

        [Header("UI")]
        [SerializeField]
        private GameObject levelStationPrefab;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue currentLevel;

        [SerializeField]
        private UIntValue latestLevel;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            for (int i = 0; i < latestLevel.Value * 3; ++i)
            {
                GameObject.Instantiate(levelStationPrefab, transform);
            }
        }

        #endregion
    }
}

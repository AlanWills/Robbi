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
        private GameObject levelStationPrefab;

        [Header("Parameters")]
        [SerializeField]
        private UIntValue latestUnlockedLevel;

        [SerializeField]
        private UIntValue latestAvailableLevel;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            Instantiate(Math.Min(latestUnlockedLevel.Value, latestAvailableLevel.Value) + 1);
        }
            
        #endregion

        #region Level Station Methods

        public void Instantiate(uint count)
        {
            // Do this in reverse order so the latest level always appear in the lower right
            for (uint i = count; i > 0; --i)
            {
                GameObject levelStationGameObject = GameObject.Instantiate(levelStationPrefab, transform);
                LevelStation levelStation = levelStationGameObject.GetComponent<LevelStation>();
                levelStation.Index = (uint)transform.childCount - 1;
                levelStation.Complete = levelStation.Index < latestUnlockedLevel.Value;
            }
        }

        public void Clear()
        {
            for (int i = transform.childCount; i > 0; --i)
            {
                GameObject.DestroyImmediate(transform.GetChild(i - 1).gameObject);
            }
        }

        #endregion
    }
}

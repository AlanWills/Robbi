using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Tutorials
{
    [AddComponentMenu("Robbi/Tutorials/Tutorial")]
    public class Tutorial : MonoBehaviour
    {
        #region Properties and Fields

        public UIntValue currentLevel;
        public UIntValue latestUnlockedLevel;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            gameObject.SetActive(currentLevel.Value == latestUnlockedLevel.Value);
        }

        #endregion
    }
}

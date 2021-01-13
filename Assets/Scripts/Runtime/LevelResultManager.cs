using Celeste.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Level Result Manager")]
    public class LevelResultManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private FSMRuntime levelResultFSM;

        #endregion

        #region Initialize/Cleanup

        public void Initialize() { }

        public void Cleanup() { }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (levelResultFSM == null)
            {
                levelResultFSM = GetComponent<FSMRuntime>();
            }
        }

        #endregion
    }
}

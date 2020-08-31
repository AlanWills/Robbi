using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Robbi.Exit
{
    [Serializable]
    [CreateAssetMenu(fileName = "Exit", menuName = "Robbi/Exit/Exit")]
    public class Exit : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int position;
        public Events.Event onExitReached;

        #endregion

        #region Exit Checking

        public void TryExit(Vector3Int speculativePosition)
        {
            if (speculativePosition == position)
            {
                onExitReached.Raise();
            }
        }

        #endregion
    }
}

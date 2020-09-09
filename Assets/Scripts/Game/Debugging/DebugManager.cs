using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Debugging
{
    [AddComponentMenu("Robbi/Debugging/DebugManager")]
    public class DebugManager : MonoBehaviour
    {
        #region Unity Methods

        private void Start()
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            DontDestroyOnLoad(gameObject);
#else
            GameObject.Destroy(gameObject);
#endif
        }

        #endregion
    }
}

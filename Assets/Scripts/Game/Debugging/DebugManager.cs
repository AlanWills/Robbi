using Robbi.Parameters;
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
        #region Properties and Fields

        [SerializeField]
        private BoolValue isDebugBuild;

        #endregion

        #region Unity Methods

        private void Start()
        {
            if (isDebugBuild.Value)
            {
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }
        }

        #endregion
    }
}

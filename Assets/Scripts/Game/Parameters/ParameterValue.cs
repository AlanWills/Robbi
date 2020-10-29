using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [Serializable]
    public class ParameterValue<T> : ScriptableObject
    {
        #region Properties and Fields

        [NonSerialized]
        public T value;

        [SerializeField]
        private T defaultValue;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            value = defaultValue;
        }

        #endregion
    }
}

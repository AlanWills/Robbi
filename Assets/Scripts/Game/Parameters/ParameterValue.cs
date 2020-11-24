﻿using System;
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

        public T defaultValue;

        #endregion

        #region Unity Methods
        
        private void OnEnable()
        {
            hideFlags = HideFlags.HideInHierarchy;
            value = defaultValue;
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Attributes.GUI
{
    public class HideIfAttribute : PropertyAttribute
    {
        #region Properties and Fields

        public string DependentName { get; private set; }

        #endregion

        public HideIfAttribute(string dependentName)
        {
            DependentName = dependentName;
        }
    }
}

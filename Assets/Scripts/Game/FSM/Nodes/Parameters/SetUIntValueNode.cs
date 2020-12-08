﻿using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set UInt Value")]
    [NodeWidth(250)]
    public class SetUIntValueNode : SetValueNode<uint, UIntValue, UIntReference>
    {
        #region Properties and Fields

        public SetMode setMode = SetMode.Absolute;

        #endregion

        #region FSM Runtime

        protected override void SetValue(uint newValue)
        {
            switch (setMode)
            {
                case SetMode.Absolute:
                    value.Value = newValue;
                    break;

                case SetMode.Increment:
                    value.Value += newValue;
                    break;

                case SetMode.Decrement:
                    value.Value -= newValue;
                    break;

                default:
                    Debug.LogAssertionFormat("Unhandled SetMode {0}", setMode);
                    break;
            }
        }

        #endregion
    }
}

﻿using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Parameters
{
    [Serializable]
    [CreateNodeMenu("Robbi/Parameters/Set Int Value")]
    [NodeWidth(250)]
    public class SetIntValueNode : SetValueNode<int, IntValue, IntReference>
    {
        #region Properties and Fields

        public SetMode setMode = SetMode.Absolute;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            value.value = setMode == SetMode.Absolute ? newValue.Value : value.value + newValue.Value;
        }

        #endregion
    }
}

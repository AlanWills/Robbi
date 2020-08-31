﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes
{
    [CustomNodeEditor(typeof(SceneLoaderNode))]
    public class SceneLoaderNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.2f, 0.2f, 0.6f); }
        }

        #endregion
    }
}
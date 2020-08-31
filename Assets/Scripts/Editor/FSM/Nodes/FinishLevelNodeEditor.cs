using Robbi.FSM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes
{
    [CustomNodeEditor(typeof(FinishLevelNode))]
    public class FinishLevelNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.2f, 0.2f, 0.6f); }
        }

        #endregion
    }
}

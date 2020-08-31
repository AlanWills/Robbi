using Robbi.FSM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes
{
    [CustomNodeEditor(typeof(WaitNode))]
    public class WaitNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0, 0.4f, 0); }
        }

        #endregion
    }
}

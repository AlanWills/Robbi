using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Robbi.FSM.Nodes
{
    public abstract class FSMNodeEditor : NodeEditor
    {
        #region Properties and Fields

        public virtual Color NodeColour { get { return base.GetTint(); } }

        private static Color SELECTED_COLOUR = new Color(1, 0.5f, 0);

        #endregion

        public sealed override Color GetTint()
        {
            FSMGraph fsmGraph = target.graph as FSMGraph;
            return fsmGraph.startNode == target ? SELECTED_COLOUR : NodeColour;
        }
    }
}

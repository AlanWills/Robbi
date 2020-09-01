using Robbi.FSM.Nodes.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

using Event = Robbi.Events.Event;

namespace RobbiEditor.FSM.Nodes.UI
{
    [CustomNodeEditor(typeof(ShowDialogNode))]
    public class ShowDialogNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.8f, 0.9f, 0); }
        }

        #endregion

        #region Appearance

        public override int GetWidth()
        {
            return 250;
        }

        #endregion
    }
}

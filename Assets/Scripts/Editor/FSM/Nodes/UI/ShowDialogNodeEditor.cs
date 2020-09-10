using Robbi.FSM.Nodes.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes.UI
{
    [CustomNodeEditor(typeof(ShowDialogNode))]
    public class ShowDialogNodeEditor : FSMNodeEditor
    {
        #region GUI

        public override void OnBodyGUI()
        {
            using (new LabelColourResetter(Color.black))
            {
                base.OnBodyGUI();
            }            
        }

        #endregion
    }
}

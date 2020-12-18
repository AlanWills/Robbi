using Robbi.FSM.Nodes;
using Robbi.FSM.Nodes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes.Events
{
    [CustomNodeEditor(typeof(MultiEventQueueNode))]
    public class MultiEventQueueNodeEditor : MultiEventNodeEditor
    {
        #region GUI

        public override void OnBodyGUI()
        {
            NodeEditorGUILayout.PortField(target.GetOutputPort(FSMNode.DEFAULT_OUTPUT_PORT_NAME));

            base.OnBodyGUI();
        }

        #endregion
    }
}

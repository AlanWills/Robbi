using Robbi.FSM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes
{
    [CustomNodeEditor(typeof(EventListenerNode))]
    public class EventListenerNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.8f, 0.9f, 0); }
        }

        private static GUIStyle editorLabelStyle;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            if (editorLabelStyle == null)
            {
                editorLabelStyle = new GUIStyle(EditorStyles.label);
            }

            EditorStyles.label.normal.textColor = Color.black;

            base.OnBodyGUI();

            EditorStyles.label.normal = editorLabelStyle.normal;
        }

        #endregion
    }
}

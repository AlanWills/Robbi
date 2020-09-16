using Robbi.FSM.Nodes.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace RobbiEditor.FSM.Nodes.UI
{
    [CustomNodeEditor(typeof(ShowDialogNode))]
    public class ShowDialogNodeEditor : FSMNodeEditor
    {
        #region GUI

        public override void OnBodyGUI()
        {
            serializedObject.Update();

            ShowDialogNode showDialogNode = target as ShowDialogNode;

            using (new LabelColourResetter(Color.black))
            {
                string[] excludes = { "m_Script", "graph", "position", "ports" };

                // Iterate through serialized properties and draw them like the Inspector (But with ports)
                SerializedProperty iterator = serializedObject.GetIterator();
                bool enterChildren = true;
                while (iterator.NextVisible(enterChildren))
                {
                    enterChildren = false;
                    if (excludes.Contains(iterator.name)) continue;
                    NodeEditorGUILayout.PropertyField(iterator, true);
                }

                NodeEditorGUILayout.PortField(showDialogNode.GetInputPort(ShowDialogNode.DEFAULT_INPUT_PORT_NAME));

                if (showDialogNode.parameters.showConfirmButton)
                {
                    NodeEditorGUILayout.PortField(showDialogNode.GetOutputPort(ShowDialogNode.CONFIRM_PRESSED_PORT_NAME));
                }

                if (showDialogNode.parameters.showCloseButton)
                {
                    NodeEditorGUILayout.PortField(showDialogNode.GetOutputPort(ShowDialogNode.CLOSE_PRESSED_PORT_NAME));
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

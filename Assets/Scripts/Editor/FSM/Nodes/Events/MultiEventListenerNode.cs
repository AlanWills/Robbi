using Robbi.FSM.Nodes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes.Events
{
    [CustomNodeEditor(typeof(MultiEventListenerNode))]
    public class MultiEventListenerNodeEditor : FSMNodeEditor
    {
        #region Properties and Fields

        public override Color NodeColour
        {
            get { return new Color(0.8f, 0.9f, 0); }
        }

        private static Type[] eventOptions = new Type[]
        {
            typeof(VoidEventCondition)
        };

        private int selectedEventType = 0;

        #endregion

        #region GUI

        public override void OnBodyGUI()
        {
            MultiEventListenerNode eventListenerNode = target as MultiEventListenerNode;

            if (EditorGUILayout.DropdownButton(new GUIContent("Add Event"), FocusType.Keyboard))
            {
                string[] options = eventOptions.Select(x => x.Name).ToArray();
                selectedEventType = EditorGUILayout.Popup(selectedEventType, options);

                eventListenerNode.AddEvent(eventOptions[selectedEventType]);
            }

            foreach (EventCondition eventCondition in eventListenerNode.Events)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField(eventCondition.GetType().Name);
                //NodeEditorGUILayout.PortField(eventListenerNode.);

                EditorGUILayout.EndHorizontal();
            }
        }

        #endregion
    }
}

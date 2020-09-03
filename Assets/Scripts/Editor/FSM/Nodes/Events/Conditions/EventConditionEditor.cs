using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Events.Conditions
{
    public abstract class EventConditionEditor
    {
        public void GUI(MultiEventListenerNode listenerNode, EventCondition eventCondition)
        {
            SerializedObject serializedObject = new SerializedObject(eventCondition);
            serializedObject.Update();

            OnGUI(listenerNode, serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void OnGUI(MultiEventListenerNode listenerNode, SerializedObject eventConditionObject);
    }
}

using Robbi.FSM.Nodes.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public abstract class ValueConditionEditor
    {
        public void GUI(IfNode ifNode, ValueCondition valueCondition)
        {
            SerializedObject serializedObject = new SerializedObject(valueCondition);
            serializedObject.Update();

            OnGUI(ifNode, serializedObject);

            serializedObject.ApplyModifiedProperties();
        }

        protected abstract void OnGUI(IfNode ifNode, SerializedObject eventConditionObject);
    }
}

﻿using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using Robbi.Logic;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public class AtAnySwitchConditionEditor : ValueConditionEditor
    {
        protected override void OnGUI(IfNode ifNode, SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "At Any Switch",
                "At No Switches"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals
            };

            Vector3IntReference reference = (valueCondition.targetObject as AtAnySwitchCondition).target;
            DrawDefaultGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}
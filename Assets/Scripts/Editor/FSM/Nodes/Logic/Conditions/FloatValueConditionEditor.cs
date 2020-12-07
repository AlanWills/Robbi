﻿using Robbi.FSM.Nodes.Logic;
using Robbi.FSM.Nodes.Logic.Conditions;
using Robbi.Logic;
using Robbi.Parameters;
using RobbiEditor.PropertyDrawers.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.FSM.Nodes.Logic.Conditions
{
    public class FloatValueConditionEditor : ValueConditionEditor
    {
        protected override void OnGUI(IfNode ifNode, SerializedObject valueCondition)
        {
            string[] operatorDisplayNames = new string[]
            {
                "Equals",
                "Not Equals",
                "Less Than",
                "Less Than Or Equal To",
                "Greater Than",
                "Greater Than Or Equal To"
            };

            int[] operators = new int[]
            {
                (int)ConditionOperator.Equals,
                (int)ConditionOperator.NotEquals,
                (int)ConditionOperator.LessThan,
                (int)ConditionOperator.LessThanOrEqualTo,
                (int)ConditionOperator.GreaterThan,
                (int)ConditionOperator.GreaterThanOrEqualTo,
            };

            FloatReference reference = (valueCondition.targetObject as FloatValueCondition).target;
            DrawDefaultGUI(valueCondition, operatorDisplayNames, operators, reference);
        }
    }
}
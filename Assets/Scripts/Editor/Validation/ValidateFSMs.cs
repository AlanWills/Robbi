﻿using Robbi.FSM;
using Robbi.Utils;
using RobbiEditor.Validation.FSM;
using RobbiEditor.Validation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Validation
{
    public static class ValidateFSMs
    {
        [MenuItem("Robbi/Validation/Validate FSMs")]
        public static void MenuItem()
        {
            Validate.MenuItem<FSMGraph>();
        }
    }
}

using Robbi.FSM;
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
        [MenuItem("Robbi/Validation/FSMs/Find")]
        public static void FindMenuItem()
        {
            Validate.Find<FSMGraph>();
        }

        [MenuItem("Robbi/Validation/FSMs/Show")]
        public static void ShowMenuItem()
        {
            Validate.Show<FSMGraph>();
        }

        [MenuItem("Robbi/Validation/FSMs/Run")]
        public static void RunMenuItem()
        {
            Validate.RunExit<FSMGraph>();
        }
    }
}

using Robbi.FSM;
using Robbi.Levels.Elements;
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
    public static class ValidateInteractableStateMachines
    {
        [MenuItem("Robbi/Validation/ISMs/Find")]
        public static void FindMenuItem()
        {
            Validate.Find<InteractableStateMachine>();
        }

        [MenuItem("Robbi/Validation/ISMs/Show")]
        public static void ShowMenuItem()
        {
            Validate.Show<InteractableStateMachine>();
        }

        [MenuItem("Robbi/Validation/ISMs/Run")]
        public static void RunMenuItem()
        {
            Validate.RunExit<InteractableStateMachine>();
        }
    }
}

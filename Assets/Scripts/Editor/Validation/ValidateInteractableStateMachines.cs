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
        [MenuItem("Robbi/Validation/Validate Interactable State Machines")]
        public static void MenuItem()
        {
            Validate.MenuItem<InteractableStateMachine>();
        }
    }
}

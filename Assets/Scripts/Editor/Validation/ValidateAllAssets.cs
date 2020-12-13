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
    public static class ValidateAllAssets
    {
        [MenuItem("Robbi/Validation/Validate All Assets")]
        public static void MenuItem()
        {
            ValidateDoors.MenuItem();
            ValidateFSMs.MenuItem();
            ValidateInteractables.MenuItem();
            ValidateInteractableStateMachines.MenuItem();
        }
    }
}

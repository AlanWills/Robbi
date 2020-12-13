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
            bool result = Validate.NoExit<Door>();
            result &= Validate.NoExit<FSMGraph>();
            result &= Validate.NoExit<Interactable>();
            result &= Validate.NoExit<InteractableStateMachine>();

            if (Application.isBatchMode)
            {
                // 0 for success
                // 1 for fail
                EditorApplication.Exit(result ? 0 : 1);
            }
            else
            {
                EditorUtility.DisplayDialog(
                    "Validation Result",
                    result ?
                        "All assets passed validation" :
                        "Some assets failed validation",
                    "OK");
            }
        }
    }
}

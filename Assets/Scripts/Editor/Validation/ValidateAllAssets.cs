using Robbi.Levels.Elements;
using UnityEditor;
using UnityEngine;
using CelesteEditor.Validation.Interfaces;
using Celeste.FSM;

namespace RobbiEditor.Validation
{
    public static class ValidateAllAssets
    {
        [MenuItem("Robbi/Validation/Validate All Assets")]
        public static void MenuItem()
        {
            bool result = Validate.RunNoExit<Door>();
            result &= Validate.RunNoExit<FSMGraph>();
            result &= Validate.RunNoExit<Interactable>();
            result &= Validate.RunNoExit<InteractableStateMachine>();

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

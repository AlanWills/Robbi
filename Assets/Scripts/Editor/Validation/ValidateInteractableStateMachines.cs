using CelesteEditor.Validation.Interfaces;
using Robbi.Levels.Elements;
using UnityEditor;

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

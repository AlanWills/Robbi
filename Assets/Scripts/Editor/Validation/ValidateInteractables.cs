using Celeste.FSM;
using CelesteEditor.Validation.Interfaces;
using Robbi.Levels.Elements;
using UnityEditor;

namespace RobbiEditor.Validation
{
    public static class ValidateInteractables
    {
        [MenuItem("Robbi/Validation/Interactables/Find")]
        public static void FindMenuItem()
        {
            Validate.Find<FSMGraph>();
        }

        [MenuItem("Robbi/Validation/Interactables/Show")]
        public static void ShowMenuItem()
        {
            Validate.Show<FSMGraph>();
        }

        [MenuItem("Robbi/Validation/Interactables/Run")]
        public static void RunMenuItem()
        {
            Validate.RunExit<Interactable>();
        }
    }
}

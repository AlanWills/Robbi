using Celeste.FSM;
using CelesteEditor.Validation.Interfaces;
using UnityEditor;

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

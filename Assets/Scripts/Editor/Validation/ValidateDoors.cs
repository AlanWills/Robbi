using CelesteEditor.Validation.Interfaces;
using Robbi.Levels.Elements;
using UnityEditor;

namespace RobbiEditor.Validation
{
    public static class ValidateDoors
    {
        [MenuItem("Robbi/Validation/Doors/Find")]
        public static void FindMenuItem()
        {
            Validate.Find<Door>();
        }

        [MenuItem("Robbi/Validation/Doors/Show")]
        public static void ShowMenuItem()
        {
            Validate.Show<Door>();
        }

        [MenuItem("Robbi/Validation/Doors/Run")]
        public static void RunMenuItem()
        {
            Validate.RunExit<Door>();
        }
    }
}

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

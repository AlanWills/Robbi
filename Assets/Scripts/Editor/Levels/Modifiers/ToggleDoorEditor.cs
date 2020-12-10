using Robbi.Events.Levels.Elements;
using Robbi.Levels.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Levels.Modifiers
{
    [CustomEditor(typeof(ToggleDoor))]
    public class ToggleDoorEditor : LevelModifierEditor
    {
        #region Unity Methods

        private void OnEnable()
        {
            ToggleDoor toggeDoor = target as ToggleDoor;
            if (toggeDoor.doorEvent == null)
            {
                toggeDoor.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                EditorUtility.SetDirty(toggeDoor);
            }
        }

        #endregion
    }
}
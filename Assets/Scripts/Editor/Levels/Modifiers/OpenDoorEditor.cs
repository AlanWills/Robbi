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
    [CustomEditor(typeof(OpenDoor))]
    public class OpenDoorEditor : LevelModifierEditor
    {
        #region Unity Methods

        private void OnEnable()
        {
            OpenDoor openDoor = target as OpenDoor;
            if (openDoor.doorEvent == null)
            {
                openDoor.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_OPENED_EVENT);
                EditorUtility.SetDirty(openDoor);
            }
        }

        #endregion
    }
}
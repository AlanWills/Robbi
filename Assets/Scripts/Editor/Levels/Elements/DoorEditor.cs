using Celeste.Tools;
using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        #region Menu Items

        [MenuItem("Assets/Create/Robbi/Levels/Door")]
        public static void CreateDoor()
        {
            CreateDoor("Door", EditorOnly.GetSelectionObjectPath(), DoorState.Closed);
        }

        #endregion

        #region Utility

        public static void CreateDoor(string name, string path, DoorState startingState)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.name = name;
            door.startingState = startingState;

            EditorOnly.CreateAsset(door, path);
            door.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
        }

        #endregion
    }
}

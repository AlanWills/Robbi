using CelesteEditor.Tools;
using Robbi.Levels.Elements;
using RobbiEditor.Constants;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Levels.Elements
{
    [CustomEditor(typeof(Door))]
    public class DoorEditor : Editor
    {
        #region Menu Items

        [MenuItem("Assets/Create/Robbi/Level/Door")]
        public static void CreateDoor()
        {
            CreateDoor("Door", AssetUtility.GetSelectionObjectPath(), DoorState.Closed);
        }

        #endregion

        #region Utility

        public static void CreateDoor(string name, string path, DoorState startingState)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.name = name;
            door.startingState = startingState;

            AssetUtility.CreateAsset(door, path);
            door.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
        }

        #endregion
    }
}

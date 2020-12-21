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

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Green")]
        public static void CreateHorizontalGreenDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Red")]
        public static void CreateHorizontalRedDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Blue")]
        public static void CreateHorizontalBlueDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Horizontal Doors/Grey")]
        public static void CreateHorizontalGreyDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Horizontal, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Green")]
        public static void CreateVerticalGreenDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Red")]
        public static void CreateVerticalRedDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Blue")]
        public static void CreateVerticalBlueDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical, DoorState.Closed);
        }

        [MenuItem("Assets/Create/Robbi/Vertical Doors/Grey")]
        public static void CreateVerticalGreyDoor()
        {
            CreateDoor("Door", GetSelectionObjectPath(), Direction.Vertical, DoorState.Closed);
        }

        #endregion

        #region Utility

        public static void CreateDoor(string name, string path, Direction direction, DoorState startingState)
        {
            Door door = ScriptableObject.CreateInstance<Door>();
            door.name = name;
            door.direction = direction;
            door.startingState = startingState;

            AssetUtility.CreateAsset(door, path);
            door.SetAddressableInfo(AddressablesConstants.LEVELS_GROUP);
        }

        private static string GetSelectionObjectPath()
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            return path;
        }

        #endregion
    }
}

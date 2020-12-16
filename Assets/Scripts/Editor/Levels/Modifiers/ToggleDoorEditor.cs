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
        #region Properties and Fields

        private SerializedProperty doorEventProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            ToggleDoor toggleDoor = target as ToggleDoor;
            if (toggleDoor.doorEvent == null)
            {
                toggleDoor.doorEvent = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
                EditorUtility.SetDirty(toggleDoor);
            }

            doorEventProperty = serializedObject.FindProperty("doorEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "doorEvent", "m_Script");

            if (doorEventProperty.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(doorEventProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
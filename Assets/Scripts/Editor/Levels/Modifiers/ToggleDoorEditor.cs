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
            serializedObject.Update();

            doorEventProperty = serializedObject.FindProperty("doorEvent");
            if (doorEventProperty != null && doorEventProperty.objectReferenceValue == null)
            {
                doorEventProperty.objectReferenceValue = AssetDatabase.LoadAssetAtPath<DoorEvent>(EventFiles.DOOR_TOGGLED_EVENT);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (doorEventProperty == null)
            {
                DrawPropertiesExcluding(serializedObject, "doorEvent", "m_Script", "doorEvent");
            }
            else
            {
                DrawPropertiesExcluding(serializedObject, "doorEvent", "m_Script");

                if (doorEventProperty.objectReferenceValue == null)
                {
                    EditorGUILayout.PropertyField(doorEventProperty);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
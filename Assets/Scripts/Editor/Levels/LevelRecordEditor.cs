using Robbi.Levels;
using Celeste.Parameters;
using UnityEditor;
using UnityEngine;
using CelesteEditor;

namespace RobbiEditor.Levels
{
    [CustomEditor(typeof(LevelRecord))]
    public class LevelRecordEditor : Editor
    {
        #region Properties and Fields

        private SerializedProperty latestAvailableLevelProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            latestAvailableLevelProperty = serializedObject.FindProperty("latestAvailableLevel");
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (latestAvailableLevelProperty.objectReferenceValue != null)
            {
                UIntValue latestLevelValue = latestAvailableLevelProperty.objectReferenceValue as UIntValue;
                latestLevelValue.DefaultValue = CelesteEditorGUILayout.UIntField("Latest Available Level", latestLevelValue.DefaultValue);
            }

            DrawPropertiesExcluding(serializedObject, "m_Script");

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

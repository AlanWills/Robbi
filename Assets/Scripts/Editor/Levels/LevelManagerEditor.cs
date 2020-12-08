using Robbi.Levels;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Levels
{
    [CustomEditor(typeof(LevelManager))]
    public class LevelManagerEditor : Editor
    {
        #region Properties and Fields

        private SerializedProperty latestLevelIndexProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            latestLevelIndexProperty = serializedObject.FindProperty("latestLevelIndex");
        }

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "latestLevelIndex");

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(latestLevelIndexProperty);

            if (latestLevelIndexProperty.objectReferenceValue != null)
            {
                UIntValue latestLevelValue = latestLevelIndexProperty.objectReferenceValue as UIntValue;
                latestLevelValue.DefaultValue = RobbiEditorGUILayout.UIntField(GUIContent.none, latestLevelValue.DefaultValue);
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

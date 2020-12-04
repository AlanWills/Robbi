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
                EditorGUI.BeginChangeCheck();
                UIntValue latestLevelValue = latestLevelIndexProperty.objectReferenceValue as UIntValue;
                latestLevelValue.defaultValue = RobbiEditorGUILayout.UIntField(GUIContent.none, latestLevelValue.defaultValue);

                if (EditorGUI.EndChangeCheck())
                {
                    EditorUtility.SetDirty(latestLevelValue);
                }
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

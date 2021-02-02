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
    [CustomEditor(typeof(DeactivateLaser))]
    public class DeactivateLaserEditor : LevelModifierEditor
    {
        #region Properties and Fields

        private SerializedProperty laserEventProperty;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            serializedObject.Update();

            laserEventProperty = serializedObject.FindProperty("laserEvent");
            if (laserEventProperty != null && laserEventProperty.objectReferenceValue == null)
            {
                laserEventProperty.objectReferenceValue = AssetDatabase.LoadAssetAtPath<LaserEvent>(EventFiles.LASER_DEACTIVATED_EVENT);
            }

            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "laserEvent");
            if (laserEventProperty != null && laserEventProperty.objectReferenceValue == null)
            {
                EditorGUILayout.PropertyField(laserEventProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}
using Robbi.Levels.Modifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.Levels.Modifiers
{
    [CustomEditor(typeof(LevelModifier), true)]
    public class LevelModifierEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField(target.name, RobbiEditorStyles.BoldLabel);
            EditorGUILayout.Space();

            DrawPropertiesExcluding(serializedObject, "m_Script");
        }

        #endregion
    }
}

using Robbi.PickLevel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.PickLevel
{
    [CustomEditor(typeof(LevelStationManager))]
    public class LevelStationManagerEditor : Editor
    {
        #region Properties and Fields

        private uint instantiateCount = 20;

        #endregion

        #region GUI

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            instantiateCount = RobbiEditorGUILayout.UIntField("Instantiate Count", instantiateCount);

            if (GUILayout.Button("Instantiate", GUILayout.ExpandWidth(false)))
            {
                (target as LevelStationManager).Instantiate(instantiateCount);
            }

            if (GUILayout.Button("Clear", GUILayout.ExpandWidth(false)))
            {
                (target as LevelStationManager).Clear();
            }

            EditorGUILayout.EndHorizontal();
        }

        #endregion
    }
}

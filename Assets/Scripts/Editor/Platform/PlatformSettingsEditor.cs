using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Platform
{
    [CustomEditor(typeof(PlatformSettings), true)]
    public class PlatformSettingsEditor : Editor
    {
        #region GUI

        public override void OnInspectorGUI()
        {
            PlatformSettings platformSettings = target as PlatformSettings;

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Apply"))
            {
                platformSettings.Apply();
            }

            if (GUILayout.Button("Bump Version"))
            {
                platformSettings.BumpVersion();
            }

            EditorGUILayout.EndHorizontal();
            
            base.OnInspectorGUI();
        }

        #endregion
    }
}

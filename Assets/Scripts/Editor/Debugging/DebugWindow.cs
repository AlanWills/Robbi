using Robbi.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace RobbiEditor.Debugging
{
    public class DebugWindow : EditorWindow
    {
        #region GUI

        private void OnGUI()
        {
            GUIStyle boldText = new GUIStyle(GUI.skin.label);
            boldText.fontStyle = FontStyle.Bold;
            EditorGUILayout.LabelField("Level", boldText);

            EditorGUI.BeginChangeCheck();
            LevelManager levelManager = LevelManager.Load();
            levelManager.CurrentLevelIndex = RobbiEditorGUILayout.UIntField(new GUIContent("Current Level Index", "The 0-based index for the current level we are on"), levelManager.CurrentLevelIndex);

            if (EditorGUI.EndChangeCheck())
            {
                levelManager.Save();
            }
        }

        #endregion

        #region Menu Item

        [MenuItem("Window/Robbi/Debug/Debug Window")]

        public static void ShowDebugWindow()
        {
            EditorWindow.GetWindow<DebugWindow>(false, "Debug Window");
        }

        #endregion
    }
}

using Celeste.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CelesteEditor.Tilemaps.WaveFunctionCollapse
{
    [CustomEditor(typeof(TileDescription))]
    public class TileDescriptionEditor : Editor
    {
        #region Properties and Fields

        private TileDescription TileDescription
        {
            get { return target as TileDescription; }
        }

        private SerializedProperty rulesProperty;
        private Vector2 scrollPosition;

        #endregion

        #region GUI Methods

        private void OnEnable()
        {
            rulesProperty = serializedObject.FindProperty("rules");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Rule", GUILayout.ExpandWidth(false)))
            {
                TileDescription.AddRule();
            }

            EditorGUILayout.EndHorizontal();
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            for (int i = rulesProperty.arraySize - 1; i >= 0; --i)
            {
                EditorGUILayout.PropertyField(rulesProperty.GetArrayElementAtIndex(i), true);

                if (GUILayout.Button("Remove Rule", GUILayout.ExpandWidth(false)))
                {
                    TileDescription.RemoveRule(i);
                }
            }

            EditorGUILayout.EndScrollView();

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

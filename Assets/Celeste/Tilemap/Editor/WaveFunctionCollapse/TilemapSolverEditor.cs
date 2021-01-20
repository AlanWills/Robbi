using Celeste.Tilemaps.WaveFunctionCollapse;
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
    [CustomEditor(typeof(TilemapSolver))]
    public class TilemapEditor : Editor
    {
        #region Properties and Fields

        private TilemapSolver TilemapSolver
        {
            get { return target as TilemapSolver; }
        }

        #endregion

        #region Editor Methods

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Find Tile Descriptions", GUILayout.ExpandWidth(false)))
            {
                FindTileDescriptions();
            }

            if (GUILayout.Button("Check Symmetric Rules", GUILayout.ExpandWidth(false)))
            {
                TilemapSolver.CheckSymmetricRules();
            }

            if (GUILayout.Button("Fix Null", GUILayout.ExpandWidth(false)))
            {
                foreach (TileDescription tileDescription in TilemapSolver.tileDescriptions)
                {
                    foreach (Rule rule in tileDescription.Rules)
                    {
                        if (rule.otherTile == null)
                        {
                            rule.otherTile = TilemapSolver.nullTile;
                            EditorUtility.SetDirty(rule);
                        }
                    }
                }

                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();
            
            base.OnInspectorGUI();
        }

        private void FindTileDescriptions()
        {
            string location = AssetDatabase.GetAssetPath(target);
            string objectFolder = Path.GetDirectoryName(location).Replace('\\', '/');
            objectFolder = objectFolder.EndsWith("/") ? objectFolder.Substring(0, objectFolder.Length - 1) : objectFolder;
            string[] objectGuids = AssetDatabase.FindAssets("t:" + typeof(TileDescription).Name, new string[] { objectFolder });

            serializedObject.Update();

            SerializedProperty objectsProperty = serializedObject.FindProperty("tileDescriptions");
            bool dirty = false;

            if (objectsProperty.arraySize != objectGuids.Length)
            {
                objectsProperty.arraySize = objectGuids.Length;
            }

            for (int i = 0; i < objectGuids.Length; ++i)
            {
                string objectPath = AssetDatabase.GUIDToAssetPath(objectGuids[i]);
                TileDescription obj = AssetDatabase.LoadAssetAtPath<TileDescription>(objectPath);

                SerializedProperty arrayElement = objectsProperty.GetArrayElementAtIndex(i);

                if (obj != arrayElement.objectReferenceValue)
                {
                    arrayElement.objectReferenceValue = obj;
                    dirty = true;
                }
            }

            serializedObject.ApplyModifiedProperties();

            if (dirty)
            {
                EditorUtility.SetDirty(target);
            }
        }

        #endregion
    }
}

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
            base.OnInspectorGUI();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Find Tile Descriptions", GUILayout.ExpandWidth(false)))
            {
                FindTileDescriptions();
            }

            if (GUILayout.Button("Sort Tile Descriptions (H -> L)", GUILayout.ExpandWidth(false)))
            {
                TilemapSolver.SortTileDescriptions();
                EditorUtility.SetDirty(TilemapSolver);
            }

            if (GUILayout.Button("Check Symmetric Rules", GUILayout.ExpandWidth(false)))
            {
                CheckSymmetricRules();
            }

            EditorGUILayout.EndHorizontal();
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

        private void CheckSymmetricRules()
        {
            foreach (TileDescription thisTileDescription in TilemapSolver.tileDescriptions)
            {
                foreach (Rule rule in thisTileDescription.Rules)
                {
                    if (thisTileDescription == rule.otherTile || rule.otherTile == null)
                    {
                        continue;
                    }

                    Rule ruleAboutThisTile = rule.otherTile.FindRule(x => x.otherTile == thisTileDescription && x.direction == rule.direction.Opposite());
                    if (ruleAboutThisTile != null)
                    {
                        // We have the symmetric rule in the other tile description
                        continue;
                    }

                    Debug.LogErrorFormat("Missing symmetric rule {0}-{1} for this tile {2}-{3}", rule.direction.Opposite(), rule.otherTile.name, rule.direction, thisTileDescription.name);
                }
            }
        }

        #endregion
    }
}

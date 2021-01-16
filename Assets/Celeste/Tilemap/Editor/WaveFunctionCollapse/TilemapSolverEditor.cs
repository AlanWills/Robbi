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

        private TilemapSolver Tilemap
        {
            get { return target as TilemapSolver; }
        }

        private bool useCustomStartPoint;
        private int customStartPointX;
        private int customStartPointY;
        
        private int collapseLocationX;
        private int collapseLocationY;

        private bool tileInfoSectionOpen = false;

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

            if (GUILayout.Button("Add Symmetric Rules", GUILayout.ExpandWidth(false)))
            {
                AddSymmetricRules();
            }

            EditorGUILayout.EndHorizontal();

            useCustomStartPoint = EditorGUILayout.Toggle("Custom Start Point", useCustomStartPoint);

            if (useCustomStartPoint)
            {
                customStartPointX = EditorGUILayout.IntField(customStartPointX);
                customStartPointY = EditorGUILayout.IntField(customStartPointY);
            }

            //if (GUILayout.Button("Create"))
            //{
            //    if (useCustomStartPoint)
            //    {
            //        Tilemap.Create(customStartPointX, customStartPointY);
            //    }
            //    else
            //    {
            //        Tilemap.Create();
            //    }
            //}

            //if (GUILayout.Button("Reset"))
            //{
            //    Tilemap.ResetSolution();
            //}

            //if (GUILayout.Button("Remove Invalid Possibilities"))
            //{
            //    Tilemap.RemoveInvalidPossibilities();
            //}

            //if (GUILayout.Button("Collapse Location"))
            //{
            //    collapseLocationX = EditorGUILayout.IntField(collapseLocationX);
            //    collapseLocationY = EditorGUILayout.IntField(collapseLocationY);

            //    Tilemap.CollapseLocation(collapseLocationX, collapseLocationY);
            //}

            //if (Tilemap.HasSolutionInfo)
            //{
            //    tileInfoSectionOpen = EditorGUILayout.Foldout(tileInfoSectionOpen, "Tile Info");
            //    if (tileInfoSectionOpen)
            //    {
            //        TilemapSolver tilemap = Tilemap;

            //        for (int row = 0; row < tilemap.rows; ++row)
            //        {
            //            rowOpen[row] = EditorGUILayout.Foldout(rowOpen[row], "Row: " + row.ToString());

            //            if (rowOpen[row])
            //            {
            //                List<TilePossibilities> rowPossibilities = tilemap.Solution[row];

            //                for (int column = 0, nColumns = Math.Min(rowPossibilities.Count, tilemap.columns); column < nColumns; ++column)
            //                {
            //                    int columnIndex = row * tilemap.columns + column;
            //                    columnOpen[columnIndex] = EditorGUILayout.Foldout(columnOpen[columnIndex], "Column: " + column.ToString());

            //                    if (columnOpen[columnIndex])
            //                    {
            //                        foreach (Tile tile in rowPossibilities[column].possibleTiles)
            //                        {
            //                            EditorGUILayout.LabelField(tile.name);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
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

        private void AddSymmetricRules()
        {
            foreach (TileDescription thisTileDescription in Tilemap.tileDescriptions)
            {
                foreach (TileDescription otherTileDescription in Tilemap.tileDescriptions)
                {
                    if (thisTileDescription == otherTileDescription)
                    {
                        continue;
                    }

                    Rule ruleAboutOtherTile = thisTileDescription.FindRule(x => x.otherTile == otherTileDescription);
                    if (ruleAboutOtherTile == null)
                    {
                        // We don't have a rule about the other tile, so forget about it
                        continue;
                    }

                    Rule ruleAboutThisTile = otherTileDescription.FindRule(x => x.otherTile == thisTileDescription);
                    if (ruleAboutThisTile != null && ruleAboutThisTile.direction == ruleAboutOtherTile.direction.Opposite())
                    {
                        // We have the symmetric rule in the other tile description
                        continue;
                    }

                    // We add a symmetric rule to the other tile for the one we have in this tile description
                    Rule symmetricRule = otherTileDescription.AddRule();
                    symmetricRule.direction = ruleAboutOtherTile.direction.Opposite();
                    symmetricRule.otherTile = thisTileDescription;
                    EditorUtility.SetDirty(symmetricRule);
                }
            }
        }

        #endregion
    }
}

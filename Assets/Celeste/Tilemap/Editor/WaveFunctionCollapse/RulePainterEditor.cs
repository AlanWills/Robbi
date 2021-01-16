﻿using Celeste.Tilemaps.WaveFunctionCollapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CelesteEditor.Tilemaps.WaveFunctionCollapse
{
    [CustomEditor(typeof(RulePainter))]
    public class RulePainterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawPropertiesExcluding(serializedObject, "m_Script", "tileDescription");

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("tileDescription"));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                ShowRules();
            }

            EditorGUILayout.BeginHorizontal();

            RulePainter rulePainter = target as RulePainter;

            if (GUILayout.Button("Show Rules", GUILayout.ExpandWidth(false)))
            {
                ShowRules();
            }

            if (GUILayout.Button("New Rule", GUILayout.ExpandWidth(false)))
            {
                int index = 0;
                while (rulePainter.tilemap.HasTile(new Vector3Int(index * 3, 0, 0)))
                {
                    ++index;
                }

                rulePainter.tilemap.SetTile(new Vector3Int(index * 3, 0, 0), rulePainter.tileDescription.tile);
            }

            if (GUILayout.Button("Save Rules", GUILayout.ExpandWidth(false)))
            {
                Tilemap tilemap = rulePainter.tilemap;
                TileDescription tileDescription = rulePainter.tileDescription;
                tileDescription.ClearRules();

                int index = 0;
                while (tilemap.HasTile(new Vector3Int(index * 3, 0, 0)))
                {
                    Vector3Int currentTilePosition = new Vector3Int(index * 3, 0, 0);
                    Rule rule = tileDescription.AddRule();
                    
                    if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(1, 0, 0), Direction.LeftOf, rule))
                    {
                        // Check LeftOf relationship (to the right)
                    }
                    else if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(-1, 0, 0), Direction.RightOf, rule))
                    {
                        // Check RightOf relationship (to the left)
                    }
                    else if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(0, -1, 0), Direction.Above, rule))
                    {
                        // Check Above relationship (to the bottom)
                    }
                    else if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(0, 1, 0), Direction.Below, rule))
                    {
                        // Check Below relationship (to the top)
                    }

                    ++index;
                }

                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowRules()
        {
            RulePainter rulePainter = target as RulePainter;
            Tilemap tilemap = rulePainter.tilemap;
            TileDescription tileDescription = rulePainter.tileDescription;

            tilemap.ClearAllTiles();

            int index = 0;
            foreach (Rule rule in tileDescription.Rules)
            {
                Vector3Int position = new Vector3Int(index * 3, 0, 0);
                tilemap.SetTile(position, tileDescription.tile);

                Vector3Int otherPosition = position;

                switch (rule.direction)
                {
                    case Direction.LeftOf:
                        otherPosition.x += 1;
                        break;

                    case Direction.RightOf:
                        otherPosition.x -= 1;
                        break;

                    case Direction.Above:
                        otherPosition.y -= 1;
                        break;

                    case Direction.Below:
                        otherPosition.y += 1;
                        break;

                    default:
                        Debug.LogAssertionFormat("Unhandled direction: {0}", rule.direction);
                        break;
                }

                tilemap.SetTile(otherPosition, rule.otherTile != null ? rule.otherTile.tile : rulePainter.nullTile);
                ++index;
            }
        }

        private bool CheckDirection(Tilemap tilemap, Vector3Int currentTilePosition, Vector3Int offset, Direction direction, Rule rule)
        {
            if (tilemap.HasTile(currentTilePosition + offset))
            {
                TileBase tile = tilemap.GetTile(currentTilePosition + offset);
                rule.otherTile = tile == (target as RulePainter).nullTile ? null : (target as RulePainter).tilemapSolver.FindTileDescription(tile);
                rule.direction = direction;

                EditorUtility.SetDirty(rule);
            }

            return false;
        }
    }
}

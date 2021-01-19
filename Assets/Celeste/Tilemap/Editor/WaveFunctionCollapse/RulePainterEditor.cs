﻿using Celeste.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tools;
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
        public RulePainter RulePainter
        {
            get { return target as RulePainter; }
        }

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

            if (GUILayout.Button("Show Rules", GUILayout.ExpandWidth(false)))
            {
                ShowRules();
            }

            if (GUILayout.Button("New Rule", GUILayout.ExpandWidth(false)))
            {
                int index = 0;
                while (RulePainter.tilemap.HasTile(new Vector3Int(index * 3, 0, 0)))
                {
                    ++index;
                }

                RulePainter.tilemap.SetTile(new Vector3Int(index * 3, 0, 0), RulePainter.tileDescription.tile);
            }

            if (GUILayout.Button("New Rules For Solver", GUILayout.ExpandWidth(false)))
            {
                RulePainter.tilemap.ClearAllTiles();
                
                // +1 for null tile
                for (int i = 0; i < RulePainter.tilemapSolver.tileDescriptions.Count + 1; ++i)
                {
                    RulePainter.tilemap.SetTile(new Vector3Int(i * 3, 0, 0), RulePainter.tileDescription.tile);
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Save Rules", GUILayout.ExpandWidth(false)))
            {
                SaveRules();
            }

            if (GUILayout.Button("Apply Rules To Solver", GUILayout.ExpandWidth(false)))
            {
                SaveRules();
                ApplyRulesToSolver();
            }

            if (GUILayout.Button("Check Symmetric Rules", GUILayout.ExpandWidth(false)))
            {
                LogUtility.Clear();
                RulePainter.tilemapSolver.CheckSymmetricRules();
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        public void ShowRules()
        {
            Tilemap tilemap = RulePainter.tilemap;
            TileDescription tileDescription = RulePainter.tileDescription;

            tilemap.ClearAllTiles();

            Vector3Int latestPosition = new Vector3Int(3, 0, 0);
            Vector3Int tilePosition;
            Dictionary<TileDescription, Vector3Int> tileLookup = new Dictionary<TileDescription, Vector3Int>();

            foreach (Rule rule in tileDescription.Rules)
            {
                if (rule.otherTile == null)
                {
                    tilePosition = new Vector3Int();
                }
                else if (!tileLookup.TryGetValue(rule.otherTile, out tilePosition))
                {
                    tileLookup.Add(rule.otherTile, latestPosition);
                    tilePosition = latestPosition;
                    latestPosition.x += 3;
                }

                Vector3Int otherPosition = tilePosition;
                
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

                    case Direction.AboveLeftOf:
                        otherPosition.x += 1;
                        otherPosition.y -= 1;
                        break;

                    case Direction.AboveRightOf:
                        otherPosition.x -= 1;
                        otherPosition.y -= 1;
                        break;

                    case Direction.BelowLeftOf:
                        otherPosition.x += 1;
                        otherPosition.y += 1;
                        break;

                    case Direction.BelowRightOf:
                        otherPosition.x -= 1;
                        otherPosition.y += 1;
                        break;

                    default:
                        Debug.LogAssertionFormat("Unhandled direction: {0}", rule.direction);
                        break;
                }

                Debug.Assert(!tilemap.HasTile(otherPosition));
                tilemap.SetTile(tilePosition, tileDescription.tile);
                tilemap.SetTile(otherPosition, rule.otherTile != null ? rule.otherTile.tile : RulePainter.nullTile);
            }
        }

        private void SaveRules()
        {
            Tilemap tilemap = RulePainter.tilemap;
            TileDescription tileDescription = RulePainter.tileDescription;
            tileDescription.ClearRules();

            int index = 0;
            while (tilemap.HasTile(new Vector3Int(index * 3, 0, 0)))
            {
                Vector3Int currentTilePosition = new Vector3Int(index * 3, 0, 0);

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(1, 0, 0), Direction.LeftOf, tileDescription))
                {
                    // Check LeftOf relationship (to the right)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(-1, 0, 0), Direction.RightOf, tileDescription))
                {
                    // Check RightOf relationship (to the left)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(0, -1, 0), Direction.Above, tileDescription))
                {
                    // Check Above relationship (to the bottom)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(0, 1, 0), Direction.Below, tileDescription))
                {
                    // Check Below relationship (to the top)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(1, -1, 0), Direction.AboveLeftOf, tileDescription))
                {
                    // Check AboveLeftOf relationship (to the right)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(-1, -1, 0), Direction.AboveRightOf, tileDescription))
                {
                    // Check AboveRightOf relationship (to the left)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(1, 1, 0), Direction.BelowLeftOf, tileDescription))
                {
                    // Check BelowLeftOf relationship (to the bottom)
                }

                if (CheckDirection(tilemap, currentTilePosition, new Vector3Int(-1, 1, 0), Direction.BelowRightOf, tileDescription))
                {
                    // Check BelowRightOf relationship (to the top)
                }

                ++index;
            }

            AssetDatabase.SaveAssets();
        }

        private bool CheckDirection(
            Tilemap tilemap, 
            Vector3Int currentTilePosition, 
            Vector3Int offset, 
            Direction direction, 
            TileDescription tileDescription)
        {
            if (tilemap.HasTile(currentTilePosition + offset))
            {
                Rule rule = tileDescription.AddRule();
                TileBase tile = tilemap.GetTile(currentTilePosition + offset);
                rule.otherTile = tile == RulePainter.nullTile ? null : RulePainter.tilemapSolver.FindTileDescription(tile);
                rule.direction = direction;

                EditorUtility.SetDirty(rule);
            }

            return false;
        }

        private void ApplyRulesToSolver()
        {
            TileDescription currentTile = RulePainter.tileDescription;
            TilemapSolver tilemapSolver = RulePainter.tilemapSolver;

            foreach (Rule rule in currentTile.Rules)
            {
                if (rule.otherTile == null || rule.otherTile == currentTile)
                {
                    continue;
                }

                // We have found a rule which applies to another tile
                TileDescription otherTile = tilemapSolver.FindTileDescription(rule.otherTile.tile);
                Rule oppositeRule = otherTile.FindRule(x => x.otherTile == currentTile && x.direction == rule.direction.Opposite());
                
                if (oppositeRule == null)
                {
                    oppositeRule = otherTile.AddRule();
                    oppositeRule.direction = rule.direction.Opposite();
                    oppositeRule.otherTile = currentTile;
                    EditorUtility.SetDirty(oppositeRule);

                    Debug.LogFormat("Adding opposite rule to {0}", otherTile.name);
                }
            }

            foreach (TileDescription tileDescription in RulePainter.tilemapSolver.tileDescriptions)
            {
                if (tileDescription.tile == currentTile)
                {
                    continue;
                }

                // Try and find rules in other tiles that do not exist in this one
                Rule otherRule = tileDescription.FindRule(x => x.otherTile == currentTile);
                while (otherRule != null)
                {
                    Rule oppositeRule = currentTile.FindRule(x => x.otherTile == tileDescription && x.direction == otherRule.direction.Opposite());
                    if (oppositeRule == null)
                    {
                        tileDescription.RemoveRule(otherRule);
                    }

                    otherRule = tileDescription.FindRule(x => x.otherTile == currentTile);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}

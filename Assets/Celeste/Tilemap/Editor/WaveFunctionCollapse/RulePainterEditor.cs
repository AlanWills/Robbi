using Celeste.Tilemaps.WaveFunctionCollapse;
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

            if (GUILayout.Button("Save Rules", GUILayout.ExpandWidth(false)))
            {
                SaveRules();
            }

            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
        }

        private void ShowRules()
        {
            Tilemap tilemap = RulePainter.tilemap;
            TileDescription tileDescription = RulePainter.tileDescription;

            tilemap.ClearAllTiles();

            Vector3Int position = new Vector3Int();
            TileBase currentOther = null;

            foreach (Rule rule in tileDescription.Rules)
            {
                TileBase ruleOther = rule.otherTile == null ? null : rule.otherTile.tile;
                if (ruleOther != currentOther)
                {
                    position.x += 3;
                    currentOther = ruleOther;
                }

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
                tilemap.SetTile(position, tileDescription.tile);
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
    }
}

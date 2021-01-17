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

                    ++index;
                }

                AssetDatabase.SaveAssets();
            }

            if (GUILayout.Button("Compress Rules", GUILayout.ExpandWidth(false)))
            {
                CompressRules();
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

                if (tilemap.HasTile(position))
                {
                    position.x += 3;
                }

                tilemap.SetTile(otherPosition, rule.otherTile != null ? rule.otherTile.tile : rulePainter.nullTile);
            }
        }

        private void CompressRules()
        {
            Tilemap tilemap = (target as RulePainter).tilemap;

            TileBase centreTile = tilemap.GetTile(new Vector3Int(0, 0, 0));
            List<TileBase> leftRules = new List<TileBase>();
            List<TileBase> upRules = new List<TileBase>();
            List<TileBase> rightRules = new List<TileBase>();
            List<TileBase> downRules = new List<TileBase>();

            int index = 0;
            while (tilemap.HasTile(new Vector3Int(index * 3, 0, 0)))
            {
                Vector3Int currentTilePosition = new Vector3Int(index * 3, 0, 0);

                if (tilemap.HasTile(currentTilePosition + new Vector3Int(-1, 0, 0)))
                {
                    leftRules.Add(tilemap.GetTile(currentTilePosition + new Vector3Int(-1, 0, 0)));
                }

                if (tilemap.HasTile(currentTilePosition + new Vector3Int(1, 0, 0)))
                {
                    rightRules.Add(tilemap.GetTile(currentTilePosition + new Vector3Int(1, 0, 0)));
                }

                if (tilemap.HasTile(currentTilePosition + new Vector3Int(0, 1, 0)))
                {
                    upRules.Add(tilemap.GetTile(currentTilePosition + new Vector3Int(0, 1, 0)));
                }

                if (tilemap.HasTile(currentTilePosition + new Vector3Int(0, -1, 0)))
                {
                    downRules.Add(tilemap.GetTile(currentTilePosition + new Vector3Int(0, -1, 0)));
                }

                ++index;
            }

            for (int i = 0; i < index; ++i)
            {
                Vector3Int currentTilePosition = new Vector3Int(index * 3, 0, 0);

                tilemap.SetTile(currentTilePosition, centreTile);

                if (leftRules.Count > i)
                {
                    tilemap.SetTile(currentTilePosition + new Vector3Int(-1, 0, 0), leftRules[i]);
                }

                if (rightRules.Count > i)
                {
                    tilemap.SetTile(currentTilePosition + new Vector3Int(1, 0, 0), rightRules[i]);
                }

                if (upRules.Count > i)
                {
                    tilemap.SetTile(currentTilePosition + new Vector3Int(0, 1, 0), upRules[i]);
                }

                if (downRules.Count > i)
                {
                    tilemap.SetTile(currentTilePosition + new Vector3Int(0, -1, 0), downRules[i]);
                }
            }
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
                rule.otherTile = tile == (target as RulePainter).nullTile ? null : (target as RulePainter).tilemapSolver.FindTileDescription(tile);
                rule.direction = direction;

                EditorUtility.SetDirty(rule);
            }

            return false;
        }
    }
}

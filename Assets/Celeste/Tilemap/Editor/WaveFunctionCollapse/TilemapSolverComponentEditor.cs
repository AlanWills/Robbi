using Celeste.Tilemaps.WaveFunctionCollapse;
using CelesteEditor.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace CelesteEditor.Tilemaps.WaveFunctionCollapse
{
    [CustomEditor(typeof(TilemapSolverComponent))]
    public class TilemapSolverComponentEditor : Editor
    {
        #region Properties and Fields

        private Vector2Int tilemapBounds = new Vector2Int();

        #endregion

        private void OnEnable()
        {
            TilemapSolverComponent tilemapSolver = target as TilemapSolverComponent;
            if (tilemapSolver.tilemap != null)
            {
                tilemapBounds.x = tilemapSolver.tilemap.cellBounds.size.x;
                tilemapBounds.y = tilemapSolver.tilemap.cellBounds.size.y;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TilemapSolverComponent tilemapSolver = target as TilemapSolverComponent;

            EditorGUILayout.BeginHorizontal();

            tilemapBounds = EditorGUILayout.Vector2IntField("Tilemap Bounds", tilemapBounds);
            if (GUILayout.Button("Apply", GUILayout.ExpandWidth(false)))
            {
                tilemapSolver.tilemap.size = new Vector3Int(tilemapBounds.x, tilemapBounds.y, 1);
                tilemapSolver.tilemap.ResizeBounds();
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Reset", GUILayout.ExpandWidth(false)))
            {
                LogUtility.Clear();
                tilemapSolver.Reset();
                EditorUtility.SetDirty(tilemapSolver);
            }

            if (GUILayout.Button("Solve", GUILayout.ExpandWidth(false)))
            {
                LogUtility.Clear();
                tilemapSolver.Solve();
                EditorUtility.SetDirty(tilemapSolver);
            }

            if (GUILayout.Button("Solve Step", GUILayout.ExpandWidth(false)))
            {
                LogUtility.Clear();
                tilemapSolver.SolveStep();
                EditorUtility.SetDirty(tilemapSolver);
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}

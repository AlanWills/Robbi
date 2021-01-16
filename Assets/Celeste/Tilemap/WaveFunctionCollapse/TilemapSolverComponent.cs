using Celeste.Tilemaps.WaveFunctionCollapse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [AddComponentMenu("Celeste/Tilemaps/Wave Function Collapse/Tilemap Solver Component")]
    public class TilemapSolverComponent : MonoBehaviour
    {
        public Tilemap tilemap;
        public TilemapSolver tilemapSolver;

        private void OnValidate()
        {
            if (tilemap == null)
            {
                tilemap = GetComponent<Tilemap>();

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
#endif
            }
        }

        public void Reset()
        {
            tilemapSolver.Reset(tilemap);
        }

        public void Solve()
        {
            if (!tilemapSolver.Solve(tilemap))
            {
                Debug.LogError("No solution could be found for configuration");
            }
        }

        public void SolveStep()
        {
            if (!tilemapSolver.SolveStep(tilemap))
            {
                Debug.LogError("Step failed to solve");
            }
        }
    }
}

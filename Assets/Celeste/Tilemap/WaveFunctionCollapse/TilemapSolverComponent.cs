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
        public uint maxRetryCount = 3;

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

        public void ResetTilemap()
        {
            tilemapSolver.Reset(tilemap);
        }

        public void Solve()
        {
            uint currentRetryCount = 0;

            while (currentRetryCount < maxRetryCount)
            {
                if (tilemapSolver.Solve(tilemap))
                {
                    break;
                }
                
                ++currentRetryCount;
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

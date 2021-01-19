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

        public void SetUpFromTilemap()
        {
            tilemapSolver.SetUpFrom(tilemap);
        }

        public void Analyse(Vector2Int location)
        {
            TilePossibilities tilePossibilities = new TilePossibilities(tilemapSolver.tileDescriptions);
            tilemapSolver.UpdateFromNeighbours(location.x, location.y, tilePossibilities);

            if (tilePossibilities.HasPossibilities)
            {
                foreach (TileDescription tileDescription in tilePossibilities.possibleTiles)
                {
                    Debug.LogFormat("Possibility: {0}", tileDescription.name);
                }
            }
            else
            {
                Debug.LogError("No possibilities remaining after analysis");
            }
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
                Debug.LogAssertion("No solution could be found for configuration");
            }
        }

        public void SolveStep()
        {
            if (!tilemapSolver.SolveStep(tilemap))
            {
                Debug.LogAssertion("Step failed to solve");
            }
        }
    }
}

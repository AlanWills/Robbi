using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [CreateAssetMenu(fileName = "TilemapSolver", menuName = "Celeste/Tilemaps/Wave Function Collapse/Tilemap Solver")]
    public class TilemapSolver : ScriptableObject
    {
        #region Properties and Fields

        public bool HasSolutionInfo
        {
            get { return Solution.Count != 0; }
        }

        public List<List<TilePossibilities>> Solution { get; } = new List<List<TilePossibilities>>();

        public List<TileDescription> tileDescriptions = new List<TileDescription>();

        #endregion

        #region Tile Description

        public TileDescription FindTileDescription(TileBase tile)
        {
            TileDescription tileDescription = tileDescriptions.Find(x => x.tile == tile);
            Debug.AssertFormat(tileDescription != null, "No TileDescription found for tile {0}", tile.name);
            return tileDescription;
        }

        #endregion

        #region Solve Functions

        public bool Solve(Tilemap tilemap)
        {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            Debug.Assert(tilemapBounds.Area() > 0, "Tilemap Bounds have 0 area and will fail solving");

            Reset(tilemap);

            // IMPROVEMENT: Use entropy to order the starting positions from lowest to highest
            Vector2Int startingLocation = GetRandomLocation(tilemapBounds);
            Debug.Log(string.Format("Starting at {0}, {1}", startingLocation.x, startingLocation.y));

            while (ShouldContinueSolving(tilemapBounds))
            {
                Debug.Log(string.Format("Starting to collapse {0}, {1}", startingLocation.x, startingLocation.y));
                if (!CollapseLocation(startingLocation.x, startingLocation.y, tilemap))
                {
                    // Our algorithm has failed
                    // OPTIMIZATION: Possibility to backtrack here and collapse to a different choice
                    return false;
                }

                Vector2 location = GetLowEntropyLocation(tilemapBounds);
                startingLocation.x = (int)location.x;
                startingLocation.y = (int)location.y;
            }

            return true;
        }

        public bool SolveStep(Tilemap tilemap)
        {
            while (ShouldContinueSolving(tilemap.cellBounds))
            {
                Vector2Int randomPosition = GetRandomLocation(tilemap.cellBounds);
                Debug.Log(string.Format("Starting to collapse {0}, {1}", randomPosition.x, randomPosition.y));
                return CollapseLocation(randomPosition.x, randomPosition.y, tilemap);
            }

            return false;
        }

        public void Reset(Tilemap tilemap)
        {
            Solution.Clear();

            for (int row = 0; row < tilemap.Height(); ++row)
            {
                List<TilePossibilities> rowPossibilities = new List<TilePossibilities>();
                Solution.Add(rowPossibilities);

                for (int column = 0; column < tilemap.Width(); ++column)
                {
                    rowPossibilities.Add(new TilePossibilities(column, row, tileDescriptions));
                }
            }

            tilemap.ClearAllTilesNoResize();
        }

        public void RemoveInvalidPossibilities(BoundsInt tilemapBounds)
        {
            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    UpdateFromNeighbours(column, row);
                }
            }
        }

        public bool CollapseLocation(int x, int y, Tilemap tilemap)
        {
            Debug.Assert(y < Solution.Count);
            List<TilePossibilities> row = Solution[y];
            
            Debug.Assert(x < row.Count);
            TilePossibilities location = row[x];

            UpdateFromNeighbours(x, y);

            if (!location.HasPossibilities)
            {
                return false;
            }

            TileDescription chosenTile = location.Collapse();
            
            Debug.Assert(location.HasCollapsed, "Invalid number of possible tiles after collapse");
            Debug.Log(string.Format("Location {0}, {1} collapsed to {2}", x, y, chosenTile.name));
            tilemap.SetTile(new Vector3Int(x, y, 0), chosenTile.tile);

            UpdateNeighbours(x, y, tilemap.cellBounds, location.possibleTiles[0]);

            // If any of them end up with no possibilities this was a bad choice
            // OPTIMIZATION: Possibility to backtrack here and do another choice
            return DoAllNeighboursHavePossibilities(x, y, tilemap.cellBounds);
        }

        #endregion

        #region Entropy Functions

        private bool ShouldContinueSolving(BoundsInt tilemapBounds)
        {
            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                List<TilePossibilities> rowPossibilities = Solution[row];

                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    if (!rowPossibilities[column].HasCollapsed && rowPossibilities[column].HasPossibilities)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Vector2 GetLowEntropyLocation(BoundsInt tilemapBounds)
        {
            int x = 0, y = 0;
            int currentPossibilityCount = int.MaxValue;

            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    TilePossibilities location = Solution[row][column];

                    if (!location.HasCollapsed && location.HasPossibilities && location.possibleTiles.Count < currentPossibilityCount)
                    {
                        x = column;
                        y = row;
                        currentPossibilityCount = location.possibleTiles.Count;
                    }
                }
            }

            return new Vector2(x, y);
        }

        private Vector2Int GetRandomLocation(BoundsInt tilemapBounds)
        {
            List<int> validPositions = new List<int>();

            for (int y = 0; y < tilemapBounds.Height(); ++y)
            {
                for (int x = 0; x < tilemapBounds.Width(); ++x)
                {
                    if (!Solution[y][x].HasCollapsed && Solution[y][x].HasPossibilities)
                    {
                        validPositions.Add(y * tilemapBounds.Width() + x);
                    }
                }
            }

            int startingPositionIndex = Random.Range(0, validPositions.Count);
            return new Vector2Int(startingPositionIndex % tilemapBounds.Width(), startingPositionIndex / tilemapBounds.Width());
        }

        #endregion

        #region Neighbour Functions

        private void UpdateFromNeighbours(int x, int y)
        {
            Debug.Assert(y < Solution.Count);
            List<TilePossibilities> row = Solution[y];

            Debug.Assert(x < row.Count);
            TilePossibilities location = row[x];

            // Check left
            {
                if (x == 0)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.RightOf, null);
                }
                else if (row[x - 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.RightOf, row[x - 1].possibleTiles[0]);
                }
            }

            // Check Right
            {
                if (x == row.Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.LeftOf, null);
                }
                else if (row[x + 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.LeftOf, row[x + 1].possibleTiles[0]);
                }
            }

            // Check Bottom
            {
                if (y == 0)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.Above, null);
                }
                else if (Solution[y - 1][x].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.Above, Solution[y - 1][x].possibleTiles[0]);
                }
            }

            // Check Top
            {
                if (y == Solution.Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.Below, null);
                }
                else if (Solution[y + 1][x].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesFor(Direction.Below, Solution[y + 1][x].possibleTiles[0]);
                }
            }
        }

        private void UpdateNeighbours(int x, int y, BoundsInt tilemapBounds, TileDescription collapsedTile)
        {
            // IMPROVEMENT: Allow holes - not necessarily all neighbours need a gap.  Or make a null tile which you have to specify rules for too?

            if (x != 0)
            {
                Solution[y][x - 1].RemoveUnsupportedPossibilitiesFor(Direction.LeftOf, collapsedTile);
            }

            if (x < tilemapBounds.Width() - 1)
            {
                Solution[y][x + 1].RemoveUnsupportedPossibilitiesFor(Direction.RightOf, collapsedTile);
            }

            if (y != 0)
            {
                Solution[y - 1][x].RemoveUnsupportedPossibilitiesFor(Direction.Below, collapsedTile);
            }

            if (y < tilemapBounds.Height() - 1)
            {
                Solution[y + 1][x].RemoveUnsupportedPossibilitiesFor(Direction.Above, collapsedTile);
            }
        }

        private bool DoAllNeighboursHavePossibilities(int x, int y, BoundsInt tilemapBounds)
        {
            if (x != 0 && !Solution[y][x - 1].HasPossibilities)
            {
                return false;
            }

            if (x < tilemapBounds.Width() - 1 && !Solution[y][x + 1].HasPossibilities)
            {
                return false;
            }

            if (y != 0 && !Solution[y - 1][x].HasPossibilities)
            {
                return false;
            }

            if (y < tilemapBounds.Height() - 1 && !Solution[y + 1][x].HasPossibilities)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
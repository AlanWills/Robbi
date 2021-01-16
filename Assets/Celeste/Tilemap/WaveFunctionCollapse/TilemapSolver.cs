using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [CreateAssetMenu(fileName = "TilemapSolver", menuName = "Celeste/Tilemaps/Wave Function Collapse/Tilemap Solver")]
    public class TilemapSolver : ScriptableObject
    {
        #region Serialized Fields

        public List<TileDescription> tileDescriptions = new List<TileDescription>();

        #endregion

        #region Properties and Fields

        public bool HasSolutionInfo
        {
            get { return Solution.Count != 0; }
        }

        public List<List<TilePossibilities>> Solution { get; } = new List<List<TilePossibilities>>();

        #endregion

        #region Solve Functions

        public bool Solve(Tilemap tilemap)
        {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            Debug.Assert(tilemapBounds.Area() > 0, "Tilemap Bounds have 0 area and will fail solving");

            // IMPROVEMENT: Use entropy to order the starting positions from lowest to highest

            while (ShouldContinueSolving(tilemap.cellBounds))
            {
                Vector2Int startingPosition = GetRandomPosition(tilemap.cellBounds);
                if (TrySolve(tilemap, startingPosition.x, startingPosition.y))
                {
                    return true;
                }
            }

            return false;
        }

        public bool SolveStep(Tilemap tilemap)
        {
            while (ShouldContinueSolving(tilemap.cellBounds))
            {
                Vector2Int randomPosition = GetRandomPosition(tilemap.cellBounds);
                Debug.Log(string.Format("Starting to collapse {0}, {1}", randomPosition.x, randomPosition.y));
                return CollapseLocation(randomPosition.x, randomPosition.y, tilemap);
            }

            return false;
        }

        public bool Solve(Tilemap tilemap, int startingPositionX, int startingPositionY)
        {
            bool result = TrySolve(tilemap, startingPositionX, startingPositionY);

            Debug.Assert(result, "No valid configurations found");
            return result;
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

        private Vector2Int GetRandomPosition(BoundsInt tilemapBounds)
        {
            List<int> validPositions = new List<int>();

            for (int y = 0; y < tilemapBounds.Height(); ++y)
            {
                for (int x = 0; x < tilemapBounds.Width(); ++x)
                {
                    if (!Solution[y][x].HasCollapsed)
                    {
                        validPositions.Add(y * tilemapBounds.Width() + x);
                    }
                }
            }

            int startingPositionIndex = Random.Range(0, validPositions.Count);
            return new Vector2Int(startingPositionIndex % tilemapBounds.Width(), startingPositionIndex / tilemapBounds.Width());
        }

        private bool TrySolve(Tilemap tilemap, int startingLocationX, int startingLocationY)
        {
            Debug.Log(string.Format("Starting at {0}, {1}", startingLocationX, startingLocationY));

            Reset(tilemap);

            while (ShouldContinueSolving(tilemap.cellBounds))
            {
                Debug.Log(string.Format("Starting to collapse {0}, {1}", startingLocationX, startingLocationY));
                if (!CollapseLocation(startingLocationX, startingLocationY, tilemap))
                {
                    // Our algorithm has failed
                    // OPTIMIZATION: Possibility to backtrack here and collapse to a different choice
                    return false;
                }

                Vector2 location = NextLocation(tilemap.cellBounds);
                startingLocationX = (int)location.x;
                startingLocationY = (int)location.y;
            }

            return true;
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
                    if (!rowPossibilities[column].HasCollapsed)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private Vector2 NextLocation(BoundsInt tilemapBounds)
        {
            int x = 0, y = 0;
            int currentPossibilityCount = int.MaxValue;

            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                List<TilePossibilities> rowPossibilities = Solution[row];

                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    TilePossibilities location = rowPossibilities[column];

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
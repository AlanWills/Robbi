﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [CreateAssetMenu(fileName = "TilemapSolver", menuName = "Celeste/Tilemaps/Wave Function Collapse/Tilemap Solver")]
    public class TilemapSolver : ScriptableObject
    {
        #region Properties and Fields

        public List<List<TilePossibilities>> Solution { get; } = new List<List<TilePossibilities>>();

        public List<TileDescription> tileDescriptions = new List<TileDescription>();

        private Dictionary<TileBase, TileDescription> tileLookup = new Dictionary<TileBase, TileDescription>();
        private List<Vector2Int> lowEntropyCache = new List<Vector2Int>();

        #endregion

        private void OnEnable()
        {
            tileLookup.Clear();

            foreach (TileDescription tileDescription in tileDescriptions)
            {
                tileLookup.Add(tileDescription.tile, tileDescription);
            }
        }

        #region Utility

        public void CheckSymmetricRules()
        {
            foreach (TileDescription thisTileDescription in tileDescriptions)
            {
                foreach (Rule rule in thisTileDescription.Rules)
                {
                    if (thisTileDescription == rule.otherTile || rule.otherTile == null)
                    {
                        continue;
                    }

                    Rule ruleAboutThisTile = rule.otherTile.FindOppositeRule(thisTileDescription, rule.direction);
                    if (ruleAboutThisTile != null)
                    {
                        // We have the symmetric rule in the other tile description
                        continue;
                    }

                    Debug.LogAssertionFormat("Missing symmetric rule {0}-{1} for this tile {2}-{3}", rule.direction.Opposite(), rule.otherTile.name, rule.direction, thisTileDescription.name);
                }
            }
        }

        public TileDescription FindTileDescription(TileBase tile)
        {
            TileDescription description;
            if (!tileLookup.TryGetValue(tile, out description))
            {
                Debug.LogAssertionFormat("No TileDescription found for tile {0}", tile.name);
            }

            return description;
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
                    rowPossibilities.Add(new TilePossibilities(tileDescriptions));
                }
            }

            tilemap.ClearAllTilesNoResize();
        }

        public void SetUpFrom(Tilemap tilemap)
        {
            Solution.Clear();

            bool hasAlreadyCollapsedLocation = false;

            for (int row = 0; row < tilemap.Height(); ++row)
            {
                List<TilePossibilities> rowPossibilities = new List<TilePossibilities>();
                Solution.Add(rowPossibilities);

                for (int column = 0; column < tilemap.Width(); ++column)
                {
                    if (tilemap.HasTile(new Vector3Int(column, row, 0)))
                    {
                        TileDescription foundTileDescription = FindTileDescription(tilemap.GetTile(new Vector3Int(column, row, 0)));
                        rowPossibilities.Add(new TilePossibilities(foundTileDescription));

                        hasAlreadyCollapsedLocation = true;
                    }
                    else
                    {
                        rowPossibilities.Add(new TilePossibilities(tileDescriptions));
                    }
                }
            }

            if (hasAlreadyCollapsedLocation)
            {
                for (int row = 0; row < tilemap.Height(); ++row)
                {
                    for (int column = 0; column < tilemap.Width(); ++column)
                    {
                        Debug.AssertFormat(Solution[row][column].HasPossibilities, "({0},{1}) has no possibilities", column, row);
                        if (Solution[row][column].HasCollapsed)
                        {
                            UpdateNeighbours(column, row, tilemap.cellBounds, Solution[row][column].possibleTiles[0]);
                        }
                    }
                }
            }
        }

        #endregion

        #region Solve Functions

        public bool Solve(Tilemap tilemap)
        {
            BoundsInt tilemapBounds = tilemap.cellBounds;
            Debug.Assert(tilemapBounds.Area() > 0, "Tilemap Bounds have 0 area and will fail solving");

            SetUpFrom(tilemap);

            while (ShouldContinueSolving(tilemapBounds))
            {
                Vector2Int location = GetLowEntropyLocation(tilemapBounds);
                if (!CollapseLocation(location, tilemap))
                {
                    // Our algorithm has failed
                    // OPTIMIZATION: Possibility to backtrack here and collapse to a different choice
                    return false;
                }
            }

            return true;
        }

        public bool SolveStep(Tilemap tilemap)
        {
            if (ShouldContinueSolving(tilemap.cellBounds))
            {
                Vector2Int randomPosition = GetLowEntropyLocation(tilemap.cellBounds);
                return CollapseLocation(randomPosition, tilemap);
            }

            return false;
        }

        public IEnumerator SolveCoroutine(Tilemap tilemap)
        {
            SetUpFrom(tilemap);

            bool isRunning = SolveStep(tilemap);
            while (isRunning)
            {
                yield return null;

                for (int i = 0; i < 5 && isRunning; ++i)
                {
                    isRunning = SolveStep(tilemap);
                }
            }
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

        public bool CollapseLocation(Vector2Int locationCoords, Tilemap tilemap)
        {
            int x = locationCoords.x;
            int y = locationCoords.y;

#if UNITY_EDITOR
            Debug.Log(string.Format("Starting to collapse ({0}, {1})", x, y));
#endif
            Debug.Assert(y < Solution.Count);
            List<TilePossibilities> row = Solution[y];
            
            Debug.Assert(x < row.Count);
            TilePossibilities location = row[x];

            // Do we still need this step?
            UpdateFromNeighbours(x, y);

            if (!location.HasPossibilities)
            {
                return false;
            }

            TileDescription chosenTile = location.Collapse();
            
            Debug.Assert(location.HasCollapsed, "Invalid number of possible tiles after collapse");
#if UNITY_EDITOR
            Debug.Log(string.Format("Location {0}, {1} collapsed to {2}", x, y, chosenTile.name));
#endif
            tilemap.SetTile(new Vector3Int(x, y, 0), chosenTile.tile);

            UpdateNeighbours(x, y, tilemap.cellBounds, location.possibleTiles[0]);

            // If any of them end up with no possibilities this was a bad choice
            // OPTIMIZATION: Possibility to backtrack here and do another choice
            return DoAllNeighboursHavePossibilities(x, y, tilemap.cellBounds);
        }

        public bool IsSolved(BoundsInt tilemapBounds)
        {
            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    if (!Solution[row][column].HasCollapsed)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        #endregion

        #region Entropy Functions

        public bool ShouldContinueSolving(BoundsInt tilemapBounds)
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

        private Vector2Int GetLowEntropyLocation(BoundsInt tilemapBounds)
        {
            lowEntropyCache.Clear();

            // Fix to make random amongst lowest entropy tiles
            int lowestPossibilityCount = int.MaxValue;
            int hint = 0;

            for (int row = 0; row < tilemapBounds.Height(); ++row)
            {
                for (int column = 0; column < tilemapBounds.Width(); ++column)
                {
                    TilePossibilities location = Solution[row][column];

                    if (!location.HasCollapsed && location.HasPossibilities && location.possibleTiles.Count < lowestPossibilityCount)
                    {
                        lowestPossibilityCount = location.possibleTiles.Count;

                        if (lowestPossibilityCount == 1)
                        {
                            // Small optimisation - if lowest possibility count is 1 we will never be able to get any lower
                            hint = row * tilemapBounds.Width() + column;
                            break;
                        }
                    }
                }
            }

            for (int row = hint / tilemapBounds.Width(); row < tilemapBounds.Height(); ++row)
            {
                for (int column = hint % tilemapBounds.Width(); column < tilemapBounds.Width(); ++column)
                {
                    TilePossibilities location = Solution[row][column];

                    if (!location.HasCollapsed && location.HasPossibilities && location.possibleTiles.Count == lowestPossibilityCount)
                    {
                        lowEntropyCache.Add(new Vector2Int(column, row));
                    }
                }
            }

            Debug.Assert(lowEntropyCache.Count > 0, "No low entropy tiles could be found");
            return lowEntropyCache[Random.Range(0, lowEntropyCache.Count)];
        }

#endregion

#region Neighbour Functions

        public void UpdateFromNeighbours(int x, int y, TilePossibilities location)
        {
            Debug.AssertFormat(location != null, "Tile Possibility at ({0},{1}) is null", x, y);
            Debug.AssertFormat(y >= 0, "y: {0} is less than 0", y);
            Debug.AssertFormat(y < Solution.Count, "y: {0} is more than {1}", y, Solution.Count - 1);
            Debug.AssertFormat(x >= 0, "x: {0} is less than 0", x);
            Debug.AssertFormat(x < Solution[y].Count, "x: {0} is more than {1}", x, Solution[y].Count - 1);

            // Check left
            {
                if (x == 0)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.RightOf, null);
                }
                else if (Solution[y][x - 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.RightOf, Solution[y][x - 1].possibleTiles[0]);
                }
            }

            // Check Right
            {
                if (x == Solution[y].Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.LeftOf, null);
                }
                else if (Solution[y][x + 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.LeftOf, Solution[y][x + 1].possibleTiles[0]);
                }
            }

            // Check Bottom
            {
                if (y == 0)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.Above, null);
                }
                else if (Solution[y - 1][x].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.Above, Solution[y - 1][x].possibleTiles[0]);
                }
            }

            // Check Top
            {
                if (y == Solution.Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.Below, null);
                }
                else if (Solution[y + 1][x].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.Below, Solution[y + 1][x].possibleTiles[0]);
                }
            }

            // Check TopLeft
            {
                if (y == Solution.Count - 1 || x == 0)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.BelowRightOf, null);
                }
                else if (Solution[y + 1][x - 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.BelowRightOf, Solution[y + 1][x - 1].possibleTiles[0]);
                }
            }

            // Check TopRight
            {
                if (y == Solution.Count - 1 || x == Solution[y].Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.BelowLeftOf, null);
                }
                else if (Solution[y + 1][x + 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.BelowLeftOf, Solution[y + 1][x + 1].possibleTiles[0]);
                }
            }

            // Check BelowLeft
            {
                if (y == 0 || x == 0)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.AboveRightOf, null);
                }
                else if (Solution[y - 1][x - 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.AboveRightOf, Solution[y - 1][x - 1].possibleTiles[0]);
                }
            }

            // Check BelowRight
            {
                if (y == 0 || x == Solution[y].Count - 1)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.AboveLeftOf, null);
                }
                else if (Solution[y - 1][x + 1].HasCollapsed)
                {
                    location.RemoveUnsupportedPossibilitiesBecause(Direction.AboveLeftOf, Solution[y - 1][x + 1].possibleTiles[0]);
                }
            }
        }

        private void UpdateFromNeighbours(int x, int y)
        {
            UpdateFromNeighbours(x, y, Solution[y][x]);
        }

        private void UpdateNeighbours(int x, int y, BoundsInt tilemapBounds, TileDescription collapsedTile)
        {
            if (x < 0 || x >= tilemapBounds.Width())
            {
                Debug.LogAssertionFormat("Invalid x coordinate: {0}.  Width: {1}", x, tilemapBounds.Width());
            }

            if (y < 0 || y >= tilemapBounds.Height())
            {
                Debug.LogAssertionFormat("Invalid y coordinate: {0}.  Height: {1}", y, tilemapBounds.Height());
            }

            if (x != 0)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x - 1, y);
                Solution[y][x - 1].RemoveUnsupportedPossibilitiesBecause(Direction.LeftOf, collapsedTile);
            }

            if (x < tilemapBounds.Width() - 1)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x + 1, y);
                Solution[y][x + 1].RemoveUnsupportedPossibilitiesBecause(Direction.RightOf, collapsedTile);
            }

            if (y != 0)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x, y - 1);
                Solution[y - 1][x].RemoveUnsupportedPossibilitiesBecause(Direction.Below, collapsedTile);
            }

            if (y < tilemapBounds.Height() - 1)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x, y + 1);
                Solution[y + 1][x].RemoveUnsupportedPossibilitiesBecause(Direction.Above, collapsedTile);
            }

            if (x != 0 && y < tilemapBounds.Height() - 1)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x - 1, y + 1);
                Solution[y + 1][x - 1].RemoveUnsupportedPossibilitiesBecause(Direction.AboveLeftOf, collapsedTile);
            }

            if (x != 0 && y != 0)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x - 1, y - 1);
                Solution[y - 1][x - 1].RemoveUnsupportedPossibilitiesBecause(Direction.BelowLeftOf, collapsedTile);
            }

            if (x < tilemapBounds.Width() - 1 && y < tilemapBounds.Height() - 1)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x + 1, y + 1);
                Solution[y + 1][x + 1].RemoveUnsupportedPossibilitiesBecause(Direction.AboveRightOf, collapsedTile);
            }

            if (x < tilemapBounds.Width() - 1 && y != 0)
            {
                Debug.LogFormat("Beginning to update neighbour ({0},{1})", x + 1, y - 1);
                Solution[y - 1][x + 1].RemoveUnsupportedPossibilitiesBecause(Direction.BelowRightOf, collapsedTile);
            }
        }

        private bool DoAllNeighboursHavePossibilities(int x, int y, BoundsInt tilemapBounds)
        {
            // Left
            if (x != 0 && !Solution[y][x - 1].HasCollapsed && !Solution[y][x - 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Left of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Right
            if (x < tilemapBounds.Width() - 1 && !Solution[y][x + 1].HasCollapsed && !Solution[y][x + 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Right of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Below
            if (y != 0 && !Solution[y - 1][x].HasCollapsed && !Solution[y - 1][x].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Below ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Above
            if (y < tilemapBounds.Height() - 1 && !Solution[y + 1][x].HasCollapsed && !Solution[y + 1][x].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Above ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Above Left
            if (x != 0 && y < tilemapBounds.Height() - 1 && !Solution[y + 1][x - 1].HasCollapsed && !Solution[y + 1][x - 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Above Left of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Above Right
            if (x < tilemapBounds.Width() - 1 && y < tilemapBounds.Height() - 1 && !Solution[y + 1][x + 1].HasCollapsed && !Solution[y + 1][x + 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Above Right of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Below Left
            if (x != 0 && y != 0 && !Solution[y - 1][x - 1].HasCollapsed && !Solution[y - 1][x - 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Below Left of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            // Below Right
            if (x < tilemapBounds.Width() - 1 && y != 0 && !Solution[y - 1][x + 1].HasCollapsed && !Solution[y - 1][x + 1].HasPossibilities)
            {
                Debug.LogAssertionFormat("Location Below Right of ({0},{1}) has no possibilities", x, y);
                return false;
            }

            return true;
        }

#endregion
    }
}
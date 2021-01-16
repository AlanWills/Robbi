using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    public class TilePossibilities
    {
        #region Serialized Fields

        public List<TileDescription> possibleTiles;

        #endregion

        #region Properties and Fields

        public bool HasPossibilities
        {
            get { return possibleTiles.Count > 0; }
        }

        public bool HasCollapsed { get; private set; } = false;

        private int x;
        private int y;

        #endregion

        public TilePossibilities(int x, int y, List<TileDescription> tiles)
        {
            this.x = x;
            this.y = y;
            possibleTiles = new List<TileDescription>(tiles);
        }

        #region Possibility Functions

        public TileDescription Collapse()
        {
            // IMPROVEMENT: NEED TO USE WEIGHT

            Debug.Assert(HasPossibilities);
            TileDescription chosenTile = possibleTiles[UnityEngine.Random.Range(0, possibleTiles.Count)];

            possibleTiles.Clear();
            possibleTiles.Add(chosenTile);
            HasCollapsed = true;

            return chosenTile;
        }

        public void RemoveUnsupportedPossibilitiesFor(Direction direction, TileDescription other)
        {
            for (int i = possibleTiles.Count - 1; i >= 0; --i)
            {
                if (!possibleTiles[i].SupportsTile(other, direction))
                {
                    Debug.Log(string.Format("Removing possibility: {0} from {1}, {2} because of unsupported Tile {3} in Direction {4}", possibleTiles[i].name, x, y, other?.name, direction));
                    possibleTiles.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}
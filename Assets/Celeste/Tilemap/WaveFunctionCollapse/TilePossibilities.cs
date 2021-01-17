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
        #region Properties and Fields

        public bool HasPossibilities
        {
            get { return possibleTiles.Count > 0; }
        }

        public bool HasCollapsed { get; private set; } = false;

        public List<TileDescription> possibleTiles = new List<TileDescription>();

        #endregion

        public TilePossibilities(List<TileDescription> tiles)
        {
            possibleTiles.AddRange(tiles);
        }

        #region Possibility Functions

        public TileDescription Collapse()
        {
            Debug.Assert(HasPossibilities);

            float totalWeight = 0;
            foreach (TileDescription tileDescription in possibleTiles)
            {
                totalWeight += tileDescription.weight;
            }

            float currentWeight = 0;
            float randomChance = UnityEngine.Random.Range(0, totalWeight);
            
            foreach (TileDescription tileDescription in possibleTiles)
            {
                if (randomChance >= currentWeight && randomChance < (currentWeight + tileDescription.weight))
                {
                    possibleTiles.Clear();
                    possibleTiles.Add(tileDescription);
                    HasCollapsed = true;

                    return tileDescription;
                }

                currentWeight += tileDescription.weight;
            }

            Debug.LogAssertion("Failed to collapse a tile");
            return null;
        }

        public void RemoveUnsupportedPossibilitiesBecause(Direction direction, TileDescription other)
        {
            for (int i = possibleTiles.Count - 1; i >= 0; --i)
            {
                if (!possibleTiles[i].SupportsTile(other, direction))
                {
                    possibleTiles.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}
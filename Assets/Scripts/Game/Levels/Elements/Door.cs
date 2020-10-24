using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public class Door : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int position;
        public Direction direction;
        public Tile closedTile;
        public Tile leftOpenTile;
        public Tile rightOpenTile;

        #endregion

        #region Open/Close Methods

        public void Open(Tilemap tilemap)
        {
            tilemap.SetTile(position, null);

            if (direction == Direction.Horizontal)
            {
                tilemap.SetTile(position + new Vector3Int(-1, 0, 0), leftOpenTile);
                tilemap.SetTile(position + new Vector3Int(1, 0, 0), rightOpenTile);
            }
            else if (direction == Direction.Vertical)
            {
                tilemap.SetTile(position + new Vector3Int(0, 1, 0), leftOpenTile);
                tilemap.SetTile(position + new Vector3Int(0, -1, 0), rightOpenTile);
            }
            else
            {
                Debug.LogAssertion("Unhandled door direction");
            }
        }

        public void Close(Tilemap tilemap)
        {
            tilemap.SetTile(position, closedTile);

            if (direction == Direction.Horizontal)
            {
                tilemap.SetTile(position + new Vector3Int(-1, 0, 0), null);
                tilemap.SetTile(position + new Vector3Int(1, 0, 0), null);
            }
            else if (direction == Direction.Vertical)
            {
                tilemap.SetTile(position + new Vector3Int(0, 1, 0), null);
                tilemap.SetTile(position + new Vector3Int(0, -1, 0), null);
            }
            else
            {
                Debug.LogAssertion("Unhandled door direction");
            }
        }

        public void Toggle(Tilemap tilemap)
        {
            if (tilemap.HasTile(position))
            {
                Open(tilemap);
            }
            else
            {
                Close(tilemap);
            }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}

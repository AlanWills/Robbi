using Robbi.Tilemaps.Tiles;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Tilemaps
{
    public static class TilemapUtils
    {
        public static bool HasClosedDoor(this Tilemap tilemap, Vector3Int position)
        {
            if (tilemap.HasTile(position))
            {
                DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
                return doorTile != null && doorTile.DoorState == Levels.Elements.DoorState.Closed;
            }

            return false;
        }
    }
}
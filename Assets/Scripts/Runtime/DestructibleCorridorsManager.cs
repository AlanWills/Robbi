using Celeste.Managers;
using Celeste.Tilemaps;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Destructible Corridors Manager")]
    public class DestructibleCorridorsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue destructibleCorridorsTilemap;
        public TilemapValue movementTilemap;

        #endregion

        public void Initialize() { }

        public void Cleanup() { }

        #region Destruction Methods

        public void OnMovedFrom(Vector3Int tileCoords)
        {
            if (destructibleCorridorsTilemap.Value.HasTile(tileCoords))
            {
                destructibleCorridorsTilemap.Value.SetTile(tileCoords, null);
                movementTilemap.Value.SetTile(tileCoords, null);
            }
        }

        #endregion
    }
}

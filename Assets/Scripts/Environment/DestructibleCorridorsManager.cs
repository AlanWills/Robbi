using Celeste.Managers;
using Celeste.Tilemaps;
using UnityEngine;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Destructible Corridors Manager")]
    public class DestructibleCorridorsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue destructibleCorridorsTilemap;
        public TilemapValue movementTilemap;

        #endregion

        #region IEnvironmentManager

        public void Initialize() { }

        public void Cleanup() { }

        #endregion

        #region Destruction Methods

        public void DestroyTile(Vector3Int tileCoords)
        {
            if (destructibleCorridorsTilemap.Value.HasTile(tileCoords))
            {
                var tile = destructibleCorridorsTilemap.Value.GetTile(tileCoords);
                destructibleCorridorsTilemap.Value.SetTile(tileCoords, null);
                movementTilemap.Value.SetTile(tileCoords, null);
            }
        }

        #endregion
    }
}

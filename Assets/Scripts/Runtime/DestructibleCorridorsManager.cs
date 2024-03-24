using Celeste.Tilemaps;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Destructible Corridors Manager")]
    public class DestructibleCorridorsManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TilemapValue destructibleCorridorsTilemap;
        [SerializeField] private TilemapValue movementTilemap;

        #endregion

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

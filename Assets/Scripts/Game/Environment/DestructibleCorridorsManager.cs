using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Destructible Corridors Manager")]
    public class DestructibleCorridorsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue destructibleCorridorsTilemap;

        #endregion

        #region Destruction Methods

        public void DestroyTile(Vector3Int tileCoords)
        {
            destructibleCorridorsTilemap.value.SetTile(tileCoords, null);
        }

        #endregion
    }
}

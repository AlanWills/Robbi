﻿using Robbi.Managers;
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
        public TilemapValue movementTilemap;

        #endregion

        #region Destruction Methods

        public void DestroyTile(Vector3Int tileCoords)
        {
            if (destructibleCorridorsTilemap.value.HasTile(tileCoords))
            {
                var tile = destructibleCorridorsTilemap.value.GetTile(tileCoords);
                destructibleCorridorsTilemap.value.SetTile(tileCoords, null);
                movementTilemap.value.SetTile(tileCoords, null);
            }
        }

        #endregion
    }
}
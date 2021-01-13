﻿using Celeste.Managers;
using Celeste.Tilemaps;
using Robbi.Collecting;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Collectables Manager")]
    public class CollectablesManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue collectablesTilemap;

        private List<Collectable> collectables = new List<Collectable>();

        #endregion

        #region IEnvironmentManager

        public void Initialize(IEnumerable<Collectable> _collectables) 
        {
            collectables.Clear();
            collectables.AddRange(_collectables);
        }

        public void Cleanup() 
        {
            collectables.Clear();
        }

        #endregion

        #region Pickup Methods

        public void OnMovedTo(Vector3Int location)
        {
            foreach (Collectable collectable in collectables)
            {
                // Only pickup if we have not already done so (tilemap has value)
                if (collectable.Position == location && collectablesTilemap.Value.HasTile(location))
                {
                    PickupArgs pickupArgs = new PickupArgs();
                    collectable.Pickup(pickupArgs);
                    collectablesTilemap.Value.SetTile(collectable.Position, null);
                }
            }
        }

        #endregion
    }
}
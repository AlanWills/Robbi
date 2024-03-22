using Celeste.Tilemaps;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Collectables Manager")]
    public class CollectablesManager : MonoBehaviour
    {
        #region Properties and Fields

        public TilemapValue collectablesTilemap;

        [NonSerialized] private List<Collectable> collectables = new List<Collectable>();

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
            for (int i = 0; i < collectables.Count; ++i)
            {
                Collectable collectable = collectables[i];

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

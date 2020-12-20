using Celeste.Managers;
using Celeste.Viewport;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Collectables/Collectables Manager")]
    public class CollectablesManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue collectablesTilemap;

        private List<Collectable> collectables = new List<Collectable>();

        #endregion

        #region Collectables Management

        public void SetCollectables(IEnumerable<Collectable> _collectables)
        {
            collectables.Clear();
            collectables.AddRange(_collectables);
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

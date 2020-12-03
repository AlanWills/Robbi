using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (collectable.Position == location)
                {
                    PickupArgs pickupArgs = new PickupArgs();
                    collectable.Pickup(pickupArgs);
                    collectablesTilemap.value.SetTile(collectable.Position, null);
                }
            }
        }

        #endregion
    }
}

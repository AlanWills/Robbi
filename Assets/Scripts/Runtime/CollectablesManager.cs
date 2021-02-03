using Celeste.Managers;
using Celeste.Tilemaps;
using Robbi.Collecting;
using Robbi.Levels.Effects;
using Robbi.Levels.Elements;
using Robbi.Runtime.Actors;
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

        #region Callbacks

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            for (int i = 0; i < collectables.Count; ++i)
            {
                Collectable collectable = collectables[i];

                // Only pickup if we have not already done so (tilemap has value)
                Vector3Int tile = characterRuntime.Tile;
                if (collectable.Position == tile && collectablesTilemap.Value.HasTile(tile))
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

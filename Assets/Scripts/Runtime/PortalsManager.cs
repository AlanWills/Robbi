using Celeste.Events;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Events.Runtime.Actors;
using Robbi.Levels.Elements;
using Robbi.Runtime.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Portals Manager")]
    public class PortalsManager : MonoBehaviour
    {
        #region Properties and Fields

        public TilemapValue portalsTilemap;
        public CharacterRuntimeEvent portalEntered;
        public CharacterRuntimeEvent portalExited;

        private List<Portal> portals = new List<Portal>();

        #endregion

        public void Initialize(IEnumerable<Portal> _portals)
        {
            portals.Clear();

            foreach (Portal portal in _portals)
            {
                portals.Add(portal);
            }
        }

        public void Cleanup()
        {
            portals.Clear();
        }

        #region Unity Methods

        private void Update()
        {
            portalsTilemap.Value.RefreshAllTiles();
        }

        #endregion

        #region Interaction Methods

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            for (int i = 0; i < portals.Count; ++i)
            {
                Portal portal = portals[i];

                if (portal.IsAtEntrance(characterRuntime.Tile))
                {
                    Vector3Int exit = portal.GetExit(characterRuntime.Tile);
                    portalEntered.Invoke(characterRuntime);
                    characterRuntime.Position = portalsTilemap.Value.GetCellCenterWorld(exit);
                    portalExited.Invoke(characterRuntime);
                }
            }
        }

        #endregion
    }
}

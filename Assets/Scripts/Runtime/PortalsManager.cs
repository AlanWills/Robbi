using Celeste.Events;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Portals Manager")]
    public class PortalsManager : MonoBehaviour
    {
        #region Properties and Fields

        public TilemapValue portalsTilemap;
        public Vector3Value playerPosition;
        public Vector3IntEvent portalEntered;
        public Vector3IntEvent portalExited;

        private List<Portal> portals = new List<Portal>();

        #endregion

        #region IEnvironmentManager

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

        #endregion

        #region Unity Methods

        private void Update()
        {
            portalsTilemap.Value.RefreshAllTiles();
        }

        #endregion

        #region Interaction Methods

        public void OnMovedTo(Vector3Int location)
        {
            for (int i = 0; i < portals.Count; ++i)
            {
                Portal portal = portals[i];

                if (portal.IsAtEntrance(location))
                {
                    Vector3Int exit = portal.GetExit(location);
                    portalEntered.Raise(location);
                    playerPosition.Value = portalsTilemap.Value.GetCellCenterWorld(exit);
                    portalExited.Raise(exit);
                }
            }
        }

        #endregion
    }
}

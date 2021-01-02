using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Portals Manager")]
    public class PortalsManager : MonoBehaviour
    {
        #region Properties and Fields

        public TilemapValue portalsTilemap;
        public Vector3Value playerPosition;
        public Celeste.Events.Event levelChangedEvent;

        private List<Portal> portals = new List<Portal>();

        #endregion

        #region Unity Methods

        private void Update()
        {
            portalsTilemap.Value.RefreshAllTiles();
        }

        #endregion

        #region Interactable Management

        public void SetPortals(IEnumerable<Portal> _portals)
        {
            portals.Clear();

            foreach (Portal portal in _portals)
            {
                portals.Add(portal);
            }
        }

        #endregion

        #region Interaction Methods

        public void OnMovedTo(Vector3Int location)
        {
            foreach (Portal portal in portals)
            {
                if (portal.IsAtEntrance(location))
                {
                    playerPosition.Value = portalsTilemap.Value.GetCellCenterWorld(portal.GetExit(location));
                    levelChangedEvent.Raise();
                }
            }
        }

        #endregion
    }
}

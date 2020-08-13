using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Interactables
{
    [AddComponentMenu("Robbi/Interactables/Interactables Manager")]
    public class InteractablesManager : MonoBehaviour
    {
        #region Properties and Fields

        public Tilemap interactablesTilemap;
        public GameObject player;
        public GameObject interactableMarker;

        private List<Tuple<Vector3Int, GameObject>> activeInteractableMarkers = new List<Tuple<Vector3Int, GameObject>>();

        #endregion

        #region Unity Methods

        private void Update()
        {
            Grid grid = interactablesTilemap.layoutGrid;
            Vector3Int playerCell = grid.WorldToCell(player.transform.position);

            PurgeActiveMarkers(grid, playerCell);

            // Left
            TryAddMarker(grid, playerCell + new Vector3Int(-1, 0, 0));

            // Up
            TryAddMarker(grid, playerCell + new Vector3Int(0, 1, 0));

            // Right
            TryAddMarker(grid, playerCell + new Vector3Int(1, 0, 0));

            // Down
            TryAddMarker(grid, playerCell + new Vector3Int(0, -1, 0));
        }

        #endregion

        #region Marker Methods

        private void TryAddMarker(Grid grid, Vector3Int position)
        {
            if (interactablesTilemap.HasTile(position) && activeInteractableMarkers.Find(x => x.Item1 == position) == null)
            {
                GameObject directionMarker = GameObject.Instantiate(interactableMarker, interactablesTilemap.transform);
                directionMarker.transform.localPosition = grid.GetCellCenterLocal(position);

                activeInteractableMarkers.Add(new Tuple<Vector3Int, GameObject>(position, directionMarker));
            }
        }

        private void PurgeActiveMarkers(Grid grid, Vector3Int playerCell)
        {
            for (int i = activeInteractableMarkers.Count - 1; i >= 0; --i)
            {
                Vector3Int activeMarkerPosition = activeInteractableMarkers[i].Item1;

                if (Math.Abs(activeMarkerPosition.x - playerCell.x) + Math.Abs(activeMarkerPosition.y - playerCell.y) > 1)
                {
                    GameObject.Destroy(activeInteractableMarkers[i].Item2);
                    activeInteractableMarkers.RemoveAt(i);
                }
            }
        }

        #endregion
    }
}

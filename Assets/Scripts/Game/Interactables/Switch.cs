using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Interactables
{
    [CreateAssetMenu(fileName = "Switch", menuName = "Robbi/Interactables/Switch")]
    public class Switch : Interactable
    {
        #region Properties and Fields

        public Vector3Int doorCoords;

        #endregion

        #region Interaction Methods

        public override void Interact()
        {
            // Obviously change this
            Tilemap doorTilemap = GameObject.Find("Doors").GetComponent<Tilemap>();

            Debug.Assert(doorTilemap.HasTile(doorCoords), "Door Tilemap does not have a door at coords: " + doorCoords);
            if (doorTilemap.HasTile(doorCoords))
            {
                doorTilemap.SetTile(doorCoords, null);
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Runtime.Actors
{
    public class CharacterRuntime : MonoBehaviour
    {
        #region Tilemap Utilities

        public Vector3Int GetTile(Tilemap movementTilemap)
        {
            return movementTilemap.WorldToCell(transform.localPosition);
        }

        #endregion
    }
}

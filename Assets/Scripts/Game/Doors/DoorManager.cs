using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Doors
{
    [AddComponentMenu("Robbi/Obstacles/Door Manager")]
    public class DoorManager : MonoBehaviour
    {
        #region Properties and Fields

        public Tilemap doorTilemap;

        #endregion

        #region Door Management

        public void OpenDoor(Vector3Int doorPosition)
        {
            Debug.Assert(doorTilemap.HasTile(doorPosition), doorPosition + " not found in Door Tilemap");
            doorTilemap.SetTile(doorPosition, null);
        }

        #endregion
    }
}

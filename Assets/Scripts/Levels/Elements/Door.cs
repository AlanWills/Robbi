using Celeste.Parameters;
using Robbi.Tilemaps.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    public enum DoorState
    { 
        Opened,
        Closed
    }

    public class Door : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int position;
        public DoorState startingState = DoorState.Closed;

        #endregion

        #region Initialization

        public void Initialize(Tilemap tilemap)
        {
            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Initialize(position, startingState);
                tilemap.RefreshTile(position);
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        #endregion

        #region Open/Close Methods

        public void Open(Tilemap tilemap)
        {
            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                if (doorTile.GetDoorState(position) == DoorState.Closed)
                {
                    doorTile.Open(position);
                    tilemap.RefreshTile(position);
                }
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        public void Close(Tilemap tilemap)
        {
            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                if (doorTile.GetDoorState(position) == DoorState.Opened)
                {
                    doorTile.Close(position);
                    tilemap.RefreshTile(position);
                }
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        public void Toggle(Tilemap tilemap)
        {
            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Toggle(position);
                tilemap.RefreshTile(position);
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}

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
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    public enum DoorState
    { 
        Opened,
        Closed
    }

    public class Door : ScriptableObject
    {
        #region Properties and Fields

        public DoorState DoorState { get; private set; }

        public Vector3Int position;
        public Direction direction;
        public DoorState startingState = DoorState.Closed;

        #endregion

        #region Initialization

        public void Initialize(Tilemap tilemap)
        {
            if (startingState == DoorState.Opened)
            {
                Open(tilemap);
            }
            else if (startingState == DoorState.Closed)
            {
                Close(tilemap);
            }
            else
            {
                Debug.LogAssertionFormat("Unhandled DoorState {0} in Door.Initialize", startingState);
            }
        }

        #endregion

        #region Open/Close Methods

        public void Open(Tilemap tilemap)
        {
            DoorState = DoorState.Opened;

            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Open();
                tilemap.RefreshTile(position);
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        public void Close(Tilemap tilemap)
        {
            DoorState = DoorState.Closed;

            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Close();
                tilemap.RefreshTile(position);
            }
            else
            {
                Debug.LogAssertionFormat("No DoorTile found at {0}", position);
            }
        }

        public void Toggle(Tilemap tilemap)
        {
            if (DoorState == DoorState.Closed)
            {
                Open(tilemap);
            }
            else if (DoorState == DoorState.Opened)
            {
                Close(tilemap);
            }
            else
            {
                Debug.LogAssertionFormat("Unhandled DoorState {0} in Door.Initialize", startingState);
            }
        }

        #endregion

        public override string ToString()
        {
            return name;
        }
    }
}

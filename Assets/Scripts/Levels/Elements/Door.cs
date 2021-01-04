﻿using Celeste.Parameters;
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

        public DoorState DoorState { get; private set; }

        public Vector3Int position;
        public DoorState startingState = DoorState.Closed;

        #endregion

        #region Initialization

        public void Initialize(Tilemap tilemap)
        {
            DoorState = startingState;

            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Initialize(position, DoorState);
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
            DoorState = DoorState.Opened;

            DoorTile doorTile = tilemap.GetTile<DoorTile>(position);
            if (doorTile != null)
            {
                doorTile.Open(position);
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
                doorTile.Close(position);
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
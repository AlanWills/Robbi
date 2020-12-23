using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Tilemaps.Tiles
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Door Tile", menuName = "Robbi/Tiles/Door Tile")]
    public class DoorTile : ComplexAnimatedTile
    {
        #region Properties and Fields
        
        public DoorState DoorState { get; private set; } = DoorState.Closed;

        #endregion

        public DoorTile()
        {
            loop = false;
            playImmediately = false;
        }

        public void Initialize(DoorState doorState)
        {
            DoorState = doorState;
            reverse = DoorState == DoorState.Closed;

            switch (DoorState)
            {
                case DoorState.Opened:
                    SetAtEnd();
                    return;

                case DoorState.Closed:
                    SetAtStart();
                    return;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", DoorState, name);
                    return;
            }
        }

        #region Open/Close Methods

        public void Open()
        {
            DoorState = DoorState.Opened;
            reverse = false;
            SetAtStart();
            Play();
        }

        public void Close()
        {
            DoorState = DoorState.Closed;
            reverse = true;
            SetAtEnd();
            Play();
        }

        #endregion
    }
}
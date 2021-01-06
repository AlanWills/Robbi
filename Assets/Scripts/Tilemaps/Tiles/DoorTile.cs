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
        protected class DoorTileInstance : ComplexAnimatedTileInstance
        {
            public DoorState doorState = DoorState.Closed;
        }

        public DoorTile()
        {
            loop = false;
            playImmediately = false;
        }

        public void Initialize(Vector3Int position, DoorState doorState)
        {
            DoorTileInstance instance = AddInstance(position) as DoorTileInstance;
            instance.doorState = doorState;
            
            SetReversed(position, doorState == DoorState.Closed);

            switch (doorState)
            {
                case DoorState.Opened:
                    SetAtEnd(position);
                    return;

                case DoorState.Closed:
                    SetAtStart(position);
                    return;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", doorState, name);
                    return;
            }
        }

        protected override ComplexAnimatedTileInstance CreateInstance()
        {
            DoorTileInstance instance = new DoorTileInstance();
            instance.isPlaying = playImmediately;

            return instance;
        }

        #region Open/Close Methods

        public void Open(Vector3Int position)
        {
            DoorTileInstance instance = GetInstance(position) as DoorTileInstance;
            if (instance != null)
            {
                instance.doorState = DoorState.Opened;
            }
            
            SetReversed(position, false);
            SetAtStart(position);
            Play(position);
        }

        public void Close(Vector3Int position)
        {
            DoorTileInstance instance = GetInstance(position) as DoorTileInstance;
            if (instance != null)
            {
                instance.doorState = DoorState.Closed;
            }
            
            SetReversed(position, true);
            SetAtEnd(position);
            Play(position);
        }

        public void Toggle(Vector3Int position)
        {
            DoorTileInstance instance = GetInstance(position) as DoorTileInstance;
            if (instance == null)
            {
                Debug.LogAssertionFormat("No instance found at position {0}", position);
                return;
            }

            if (instance.doorState == DoorState.Opened)
            {
                Close(position);
            }
            else if (instance.doorState == DoorState.Closed)
            {
                Open(position);
            }
            else
            {
                Debug.LogAssertionFormat("Unhandled DoorState {0} at position {1}", instance.doorState, position);
            }
        }

        public DoorState GetDoorState(Vector3Int position)
        {
            DoorTileInstance instance = GetInstance(position) as DoorTileInstance;
            if (instance != null)
            {
                return instance.doorState;
            }

            return DoorState.Opened;
        }

        #endregion
    }
}
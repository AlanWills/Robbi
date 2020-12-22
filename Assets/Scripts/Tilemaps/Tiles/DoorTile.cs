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
    public class DoorTile : TileBase
    {
        #region Properties and Fields
        
        public DoorState DoorState { get; private set; } = DoorState.Closed;

        public AnimatedTile openToCloseTile;
        public AnimatedTile closeToOpenTile;

        #endregion

        public void Initialize(DoorState doorState)
        {
            DoorState = doorState;

            switch (DoorState)
            {
                case DoorState.Opened:
                    closeToOpenTile.SetAtEnd();
                    return;

                case DoorState.Closed:
                    openToCloseTile.SetAtEnd();
                    return;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", DoorState, name);
                    return;
            }
        }

        #region Tile Methods

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            switch (DoorState)
            {
                case DoorState.Opened:
                    closeToOpenTile.GetTileData(position, tilemap, ref tileData);
                    return;

                case DoorState.Closed:
                    openToCloseTile.GetTileData(position, tilemap, ref tileData);
                    return;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", DoorState, name);
                    return;
            }
        }

        #endregion

        #region Open/Close Methods

        public void Open()
        {
            DoorState = DoorState.Opened;
            closeToOpenTile.PlayFromStart();
        }

        public void Close()
        {
            DoorState = DoorState.Closed;
            openToCloseTile.PlayFromStart();
        }

        #endregion
    }
}
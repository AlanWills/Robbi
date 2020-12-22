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

        public TileBase openTile;
        public TileBase closeTile;

        #endregion

        public void Initialize(DoorState doorState)
        {
            this.DoorState = doorState;
        }

        #region Tile Methods

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            switch (DoorState)
            {
                case DoorState.Opened:
                    openTile.GetTileData(position, tilemap, ref tileData);
                    return;

                case DoorState.Closed:
                    closeTile.GetTileData(position, tilemap, ref tileData);
                    return;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", DoorState, name);
                    return;
            }
        }

        public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
        {
            switch (DoorState)
            {
                case DoorState.Opened:
                    openTile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
                    return true;

                case DoorState.Closed:
                    closeTile.GetTileAnimationData(position, tilemap, ref tileAnimationData);
                    return true;

                default:
                    Debug.LogAssertionFormat("Unhandled DoorState {0} in DoorTile {1}", DoorState, name);
                    return false;
            }
        }

        #endregion

        #region Open/Close Methods

        public void Open()
        {
            DoorState = DoorState.Opened;

            if (openTile is AnimatedTile)
            {
                (openTile as AnimatedTile).PlayFromStart();
            }
        }

        public void Close()
        {
            DoorState = DoorState.Closed;

            if (closeTile is AnimatedTile)
            {
                (closeTile as AnimatedTile).PlayFromStart();
            }
        }

        #endregion
    }
}
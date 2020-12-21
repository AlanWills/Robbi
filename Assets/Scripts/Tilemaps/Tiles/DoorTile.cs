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

        public Sprite openSprite;
        public Sprite closedSprite;

        #endregion

        #region Tile Methods

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.transform = Matrix4x4.identity;
            tileData.color = Color.white;
            tileData.colliderType = Tile.ColliderType.None;
            
            switch (DoorState)
            {
                case DoorState.Opened:
                    tileData.sprite = openSprite;
                    return;

                case DoorState.Closed:
                    tileData.sprite = closedSprite;
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
        }

        public void Close()
        {
            DoorState = DoorState.Closed;
        }

        #endregion
    }
}
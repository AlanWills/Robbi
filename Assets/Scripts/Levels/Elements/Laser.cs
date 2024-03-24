using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    public enum LaserDirection
    { 
        Horizontal,
        Vertical
    }

    [CreateAssetMenu(fileName = "New Laser", menuName = "Robbi/Levels/Laser")]
    public class Laser : ScriptableObject, ILevelElement
    {
        #region Properties and Fields

        public LaserDirection LaserDirection
        {
            get { return startLocation.x == endLocation.x ? LaserDirection.Vertical : LaserDirection.Horizontal; }
        }

        public bool IsActive { get; private set; } = true;

        public LaserDefinition laserDefinition;
        public Vector3Int startLocation;
        public Vector3Int endLocation;
        public bool startsActive = true;

        #endregion

        public void Initialize(Tilemap tilemap)
        {
            // Need this so that the Activate or Deactivate calls succeed
            IsActive = !startsActive;

            if (startsActive)
            {
                Activate(tilemap);
            }
            else
            {
                Deactivate(tilemap);
            }
        }

        public bool WouldAffectPosition(Vector3Int position)
        {
            if (!IsActive)
            {
                return false;
            }
            
            if (LaserDirection == LaserDirection.Horizontal)
            {
                return startLocation.y == position.y && startLocation.x <= position.x && position.x <= endLocation.x;
            }
            else
            {
                return startLocation.x == position.x && startLocation.y <= position.y && position.y <= endLocation.y;
            }
        }

        #region Activation Methods

        public void Activate(Tilemap tilemap)
        {
            if (!IsActive)
            {
                SetLaserTiles(tilemap, laserDefinition.startActiveTile, laserDefinition.middleActiveTile, laserDefinition.endActiveTile);
                IsActive = true;
            }
        }

        public void Deactivate(Tilemap tilemap)
        {
            if (IsActive)
            {
                SetLaserTiles(tilemap, laserDefinition.startInactiveTile, laserDefinition.middleInactiveTile, laserDefinition.endInactiveTile);
                IsActive = false;
            }
        }

        private void SetLaserTiles(Tilemap tilemap, TileBase startTile, TileBase middleTile, TileBase endTile)
        {
            Debug.AssertFormat(laserDefinition != null, "{0} has no Laser Definition set", name);
            tilemap.SetTile(startLocation, startTile);
            tilemap.SetTile(endLocation, endTile);

            if (LaserDirection == LaserDirection.Horizontal)
            {
                if (startLocation.x >= endLocation.x)
                {
                    Debug.LogAssertionFormat("{0} has a start location which is to the right of the end location.  This will cause a break in laser logic", name);
                    return;
                }
                else if (startLocation.y != endLocation.y)
                {
                    // Extra check for horizontal lasers since they are the fallback case
                    Debug.LogAssertionFormat("{0} has a start location which is not at the same height as the end location.  This will cause a break in laser logic", name);
                    return;
                }
            }
            else if (LaserDirection == LaserDirection.Vertical)
            {
                if (startLocation.y >= endLocation.y)
                {
                    Debug.LogAssertionFormat("{0} has a start location which is above the end location.  This will cause a break in laser logic", name);
                    return;
                }
            }

            Vector3Int diff = LaserDirection == LaserDirection.Horizontal ? new Vector3Int(1, 0, 0) : new Vector3Int(0, 1, 0);
            Vector3Int tilemapLocation = startLocation + diff;

            while (tilemapLocation != endLocation)
            {
                tilemap.SetTile(tilemapLocation, middleTile);
                tilemapLocation += diff;
            }
        }

        #endregion
    }
}

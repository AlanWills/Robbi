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
    public class Laser : ScriptableObject
    {
        #region Properties and Fields

        public LaserDirection LaserDirection
        {
            get { return startLocation.x == endLocation.x ? LaserDirection.Vertical : LaserDirection.Horizontal; }
        }

        public LaserDefinition laserDefinition;
        public Vector3Int startLocation;
        public Vector3Int endLocation;
        public bool startsActive = true;

        #endregion

        public void Initialize(Tilemap tilemap)
        {
            if (startsActive)
            {
                Activate(tilemap);
            }
            else
            {
                Deactivate(tilemap);
            }
        }

        #region Activation Methods

        public void Activate(Tilemap tilemap)
        {
            Debug.AssertFormat(laserDefinition != null, "{0} has no Laser Definition set", name);
            tilemap.SetTile(startLocation, laserDefinition.startActiveTile);
            tilemap.SetTile(endLocation, laserDefinition.endActiveTile);

            if (LaserDirection == LaserDirection.Horizontal)
            {
                if (startLocation.x >= endLocation.x)
                {
                    Debug.LogAssertionFormat("{0} has a start location which is to the right of the end location.  This will cause a break in laser logic", name);
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
                tilemap.SetTile(tilemapLocation, laserDefinition.middleActiveTile);
                tilemapLocation += diff;
            }
        }

        public void Deactivate(Tilemap tilemap)
        {
            tilemap.SetTile(startLocation, laserDefinition.startInactiveTile);
            tilemap.SetTile(endLocation, laserDefinition.endInactiveTile);

            Vector3Int diff = LaserDirection == LaserDirection.Horizontal ? new Vector3Int(1, 0, 0) : new Vector3Int(0, 1, 0);
            Vector3Int tilemapLocation = startLocation + diff;

            while (tilemapLocation != endLocation)
            {
                tilemap.SetTile(tilemapLocation, laserDefinition.middleInactiveTile);
                tilemapLocation += diff;
            }
        }

        #endregion
    }
}

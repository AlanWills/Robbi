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
            get { return startLocation.x == endLocation.x ? LaserDirection.Horizontal : LaserDirection.Vertical; }
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
            tilemap.SetTile(startLocation, laserDefinition.startActiveTile);
            tilemap.SetTile(endLocation, laserDefinition.endActiveTile);

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

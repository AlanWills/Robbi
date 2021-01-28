using Celeste.Events;
using Celeste.Managers;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System.Collections.Generic;
using UnityEngine;
using Event = Celeste.Events.Event;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Lasers Manager")]
    public class LasersManager : NamedManager
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        public TilemapValue laserTilemap;
        
        [Header("Level Lose")]
        public StringEvent levelLoseEvent;
        public StringValue laserHitReason;

        private List<Laser> lasers = new List<Laser>();

        #endregion

        #region IEnvironmentManager

        public void Initialize(IEnumerable<Laser> _lasers) 
        {
            lasers.Clear();
            lasers.AddRange(_lasers);

            foreach (Laser laser in lasers)
            {
                laser.Initialize(laserTilemap.Value);
            }
        }

        public void Cleanup() 
        {
            lasers.Clear();
        }

        #endregion

        #region Callbacks

        public void OnMovedTo(Vector3Int position)
        {
            if (laserTilemap.Value.HasTile(position))
            {
                levelLoseEvent.Raise(laserHitReason.Value);
            }
        }

        #endregion
    }
}

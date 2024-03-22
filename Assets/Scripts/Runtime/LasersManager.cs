using Celeste.Events;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Lasers Manager")]
    public class LasersManager : MonoBehaviour
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        [SerializeField] private TilemapValue laserTilemap;
        
        [Header("Level Lose")]
        [SerializeField] private StringEvent levelLostEvent;
        [SerializeField] private StringValue hitLaserReason;

        [NonSerialized] private List<Laser> lasers = new List<Laser>();

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

        private void CheckForLaserHit(Vector3Int position)
        {
            if (!laserTilemap.Value.HasTile(position))
            {
                return;
            }

            for (int i = 0; i < lasers.Count; ++i)
            {
                // Check the lasers to see if they're on
                if (lasers[i].WouldAffectPosition(position))
                {
                    levelLostEvent.Invoke(hitLaserReason.Value);
                }
            }
        }

        #endregion

        #region Callbacks

        public void OnMovedTo(Vector3Int position)
        {
            CheckForLaserHit(position);
        }

        public void OnPortalExited(Vector3Int position)
        {
            CheckForLaserHit(position);
        }

        public void OnActivateLaser(Laser laser)
        {
            laser.Activate(laserTilemap.Value);
        }

        public void OnDeactivateLaser(Laser laser)
        {
            laser.Deactivate(laserTilemap.Value);
        }

        public void OnToggleLaser(Laser laser)
        {
            laser.Toggle(laserTilemap.Value);
        }

        #endregion
    }
}

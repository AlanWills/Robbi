using Celeste.Events;
using Celeste.Managers;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using Robbi.Runtime.Actors;
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
        
        private List<Laser> lasers = new List<Laser>();
        private List<CharacterRuntime> characterRuntimes = new List<CharacterRuntime>();

        #endregion

        public void Initialize(IEnumerable<Laser> _lasers, IEnumerable<CharacterRuntime> _characterRuntimes) 
        {
            lasers.Clear();
            lasers.AddRange(_lasers);

            foreach (Laser laser in lasers)
            {
                laser.Initialize(laserTilemap.Value);
            }

            characterRuntimes.Clear();
            characterRuntimes.AddRange(_characterRuntimes);
        }

        public void Cleanup() 
        {
            lasers.Clear();
            characterRuntimes.Clear();
        }

        private void CheckForLaserHit(CharacterRuntime characterRuntime)
        {
            if (!laserTilemap.Value.HasTile(characterRuntime.Tile))
            {
                return;
            }

            for (int i = 0; i < lasers.Count; ++i)
            {
                // Check the lasers to see if they're on
                if (lasers[i].WouldAffectPosition(characterRuntime.Tile))
                {
                    characterRuntime.OnHitByLaser();
                }
            }
        }

        #region Callbacks

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            CheckForLaserHit(characterRuntime);
        }

        public void OnPortalExited(CharacterRuntime characterRuntime)
        {
            CheckForLaserHit(characterRuntime);
        }

        public void OnActivateLaser(Laser laser)
        {
            laser.Activate(laserTilemap.Value);

            foreach (CharacterRuntime characterRuntime in characterRuntimes)
            {
                CheckForLaserHit(characterRuntime);
            }
        }

        public void OnDeactivateLaser(Laser laser)
        {
            laser.Deactivate(laserTilemap.Value);
        }

        public void OnToggleLaser(Laser laser)
        {
            if (laser.IsActive)
            {
                OnDeactivateLaser(laser);
            }
            else
            {
                OnActivateLaser(laser);
            }
        }

        #endregion
    }
}

using Celeste.Managers;
using Celeste.Tilemaps;
using UnityEngine;
using Event = Celeste.Events.Event;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Exits Manager")]
    public class ExitsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue exitsTilemap;
        public Event exitReachedEvent;

        #endregion

        #region IEnvironmentManager

        public void Initialize() { }

        public void Cleanup() { }

        #endregion

        #region Exit Methods

        public void CheckForExitReached(Vector3Int position)
        {
            if (exitsTilemap.Value.HasTile(position))
            {
                exitReachedEvent.Raise();
            }
        }

        #endregion
    }
}

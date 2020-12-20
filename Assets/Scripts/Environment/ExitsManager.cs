using Celeste.Managers;
using Celeste.Viewport;
using UnityEngine;
using Event = Celeste.Events.Event;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Exits Manager")]
    public class ExitsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue exitsTilemap;
        public Event exitReachedEvent;

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

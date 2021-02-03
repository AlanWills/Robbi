using Celeste.Managers;
using Celeste.Tilemaps;
using Robbi.Runtime.Actors;
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

        public void Initialize() { }

        public void Cleanup() { }

        #region Exit Methods

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            if (exitsTilemap.Value.HasTile(characterRuntime.Tile))
            {
                exitReachedEvent.Raise();
            }
        }

        #endregion
    }
}

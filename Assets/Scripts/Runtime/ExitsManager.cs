using Celeste.Tilemaps;
using Robbi.Runtime.Actors;
using UnityEngine;
using Event = Celeste.Events.Event;

namespace Robbi.Runtime
{
    [AddComponentMenu("Robbi/Runtime/Exits Manager")]
    public class ExitsManager : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField] private TilemapValue exitsTilemap;
        [SerializeField] private Event exitReachedEvent;

        #endregion

        #region Exit Methods

        public void OnCharacterMovedTo(CharacterRuntime characterRuntime)
        {
            if (exitsTilemap.Value.HasTile(characterRuntime.Tile))
            {
                exitReachedEvent.Invoke();
            }
        }

        #endregion
    }
}

using Celeste.Tilemaps;
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

        public void CheckForExitReached(Vector3Int position)
        {
            if (exitsTilemap.Value.HasTile(position))
            {
                exitReachedEvent.Invoke();
            }
        }

        #endregion
    }
}

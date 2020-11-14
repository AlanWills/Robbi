using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Event = Robbi.Events.Event;

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
            if (exitsTilemap.value.HasTile(position))
            {
                exitReachedEvent.Raise();
            }
        }

        #endregion
    }
}

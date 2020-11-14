using Robbi.Levels.Elements;
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
    [AddComponentMenu("Robbi/Environment/Interactables Manager")]
    public class InteractablesManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue doorsTilemap;

        #endregion

        #region Interaction Methods

        public void TriggerInteraction(Interactable interactable)
        {
            interactable.Interact(doorsTilemap.value);
        }

        #endregion
    }
}

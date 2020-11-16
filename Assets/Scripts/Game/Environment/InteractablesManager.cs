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

        public List<Interactable> Interactables { get; set; }

        public TilemapValue doorsTilemap;
        public Event levelChanged;

        #endregion

        #region Interaction Methods

        public void OnMovedTo(Vector3Int location)
        {
            foreach (Interactable interactable in Interactables)
            {
                if (interactable.position == location)
                {
                    InteractArgs interact = new InteractArgs()
                    {
                        doorsTilemap = doorsTilemap.value
                    };

                    interactable.Interact(interact);
                    levelChanged.Raise();
                }
            }
        }

        #endregion
    }
}

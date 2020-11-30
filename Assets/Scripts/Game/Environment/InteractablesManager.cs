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

        public List<IInteractable> Interactables { get; set; }

        public TilemapValue doorsTilemap;

        #endregion

        #region Interaction Methods

        public void OnMovedTo(Vector3Int location)
        {
            foreach (IInteractable interactable in Interactables)
            {
                if (interactable.Position == location)
                {
                    InteractArgs interact = new InteractArgs()
                    {
                    };

                    interactable.Interact(interact);
                }
            }
        }

        public void OpenDoor(Door door)
        {
            door.Open(doorsTilemap.value);
        }

        public void CloseDoor(Door door)
        {
            door.Close(doorsTilemap.value);
        }

        public void ToggleDoor(Door door)
        {
            door.Toggle(doorsTilemap.value);
        }

        #endregion
    }
}

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

        public TilemapValue interactablesTilemap;

        private List<IInteractable> interactables = new List<IInteractable>();

        #endregion

        #region Interactable Management

        public void SetInteractables(IEnumerable<ScriptableObject> _interactables)
        {
            interactables.Clear();

            foreach (ScriptableObject scriptableObject in _interactables)
            {
                if (scriptableObject is IInteractable)
                {
                    interactables.Add(scriptableObject as IInteractable);
                }
                else
                {
                    Debug.LogAssertionFormat("SO {0} is not derived from IInteractable", scriptableObject.name);
                }
            }
        }

        #endregion

        #region Interaction Methods

        public void OnMovedTo(Vector3Int location)
        {
            foreach (IInteractable interactable in interactables)
            {
                if (interactable.Position == location)
                {
                    InteractArgs interact = new InteractArgs()
                    {
                        interactablesTilemap = interactablesTilemap.Value
                    };

                    interactable.Interact(interact);
                }
            }
        }

        #endregion
    }
}

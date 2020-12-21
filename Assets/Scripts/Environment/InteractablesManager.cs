using Robbi.Levels.Elements;
using System.Collections.Generic;
using UnityEngine;
using Celeste.Managers;
using Celeste.Tilemaps;

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
                    IInteractable interactable = scriptableObject as IInteractable;
                    interactable.Initialize();
                    interactables.Add(interactable);
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

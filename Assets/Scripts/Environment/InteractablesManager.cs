﻿using Robbi.Levels.Elements;
using System.Collections.Generic;
using UnityEngine;
using Celeste.Managers;
using Celeste.Tilemaps;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Interactables Manager")]
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractablesManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue interactablesTilemap;
        public BoxCollider2D boundingBox;
        public Celeste.Events.Event boosterUsedEvent;

        private List<IInteractable> interactables = new List<IInteractable>();

        #endregion

        #region IEnvironmentManager

        public void Initialize(IEnumerable<ScriptableObject> _interactables)
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

            Vector3Int interactablesGridSize = interactablesTilemap.Value.size;
            Vector3Int interactablesOrigin = interactablesTilemap.Value.origin;
            boundingBox.size = new Vector2(interactablesGridSize.x, interactablesGridSize.y);
            boundingBox.offset = new Vector2(interactablesOrigin.x + interactablesGridSize.x * 0.5f, interactablesOrigin.y + interactablesGridSize.y * 0.5f);
        }

        public void Cleanup()
        {
            interactables.Clear();
        }

        #endregion

        #region Callbacks

        public void OnMovedTo(Vector3Int location)
        {
            TryInteract(location);
        }
        
        public void OnGameObjectInput(Vector3 inputLocation)
        {
            Vector3Int interactableGridPosition = interactablesTilemap.Value.WorldToCell(inputLocation);
            if (!interactablesTilemap.Value.HasTile(interactableGridPosition))
            {
                // Not a valid location in the tilemap
                return;
            }

            if (TryInteract(interactableGridPosition))
            {
                boosterUsedEvent.Raise();
            }
        }

        #endregion

        #region Interaction Methods

        private bool TryInteract(Vector3Int location)
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
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region Unity Methods

        private void OnValidate()
        {
            if (boundingBox == null)
            {
                boundingBox = GetComponent<BoxCollider2D>();
            }
        }

        #endregion
    }
}

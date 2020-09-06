using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Interactables
{
    [AddComponentMenu("Robbi/Interactables/Interactable Marker")]
    [RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
    public class InteractableMarker : MonoBehaviour
    {
        #region Properties and Fields

        public Interactable interactable;
        public SpriteRenderer spriteRenderer;

        #endregion

        #region Interaction Methods

        public void UpdateVisibility(Vector3Int position)
        {
            spriteRenderer.enabled = position == interactable.position;
        }

        public void Interact()
        {
            interactable.Interact();
        }

        #endregion

        #region Unity Methods

        private void Start()
        {
            spriteRenderer.enabled = false;
        }

        #endregion
    }
}

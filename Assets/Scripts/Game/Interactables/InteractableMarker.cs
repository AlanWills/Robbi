using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Interactables
{
    [AddComponentMenu("Robbi/Interactables/Interactable Marker")]
    [RequireComponent(typeof(Collider2D))]
    public class InteractableMarker : MonoBehaviour
    {
        #region Properties and Fields

        public Interactable interactable;

        #endregion

        #region Interaction Methods

        public void Interact()
        {
            interactable.Interact();
        }

        #endregion

        #region Unity Methods

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                    if (hitInfo && hitInfo.collider == GetComponent<Collider2D>())
                    {
                        Interact();
                    }
                }
            }
#endif
        }

        private void OnMouseUpAsButton()
        {
            Interact();
        }

        #endregion
    }
}

using Robbi.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

using Event = Robbi.Events.Event;

namespace Robbi.UI
{
    [AddComponentMenu("Robbi/UI/Dialog")]
    public class Dialog : MonoBehaviour
    {
        [Serializable]
        public struct ShowDialogParams
        {
            public string title;
            public Sprite image;
            public string description;
            public string confirmButtonText;
            public bool showConfirmButton;
            public bool showCloseButton;
        }

        #region Properties and Fields

        private static int CLOSED_ANIMATION_NAME = Animator.StringToHash("Closed");

        public Event ConfirmButtonClicked
        {
            get { return confirmButtonClicked; }
        }

        public Event CloseButtonClicked
        {
            get { return closeButtonClicked; }
        }

        [SerializeField]
        private Text title;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Text description;

        [SerializeField]
        private Text confirmButtonText;

        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private Event confirmButtonClicked;

        [SerializeField]
        private Event closeButtonClicked;

        [SerializeField]
        private Animator animator;

        private Coroutine hideCoroutine;

        #endregion

        #region Show/Hide

        public void Show(ShowDialogParams showDialogParams)
        {
            if (title != null)
            {
                title.text = showDialogParams.title;
            }

            if (image != null)
            {
                image.enabled = showDialogParams.image != null;
                image.sprite = showDialogParams.image;
            }

            if (description != null)
            {
                description.text = showDialogParams.description;
            }

            if (confirmButton != null)
            {
                confirmButton.gameObject.SetActive(showDialogParams.showConfirmButton);

                if (showDialogParams.showConfirmButton)
                {
                    if (confirmButtonText != null)
                    {
                        confirmButtonText.text = showDialogParams.confirmButtonText;
                    }
                }
            }

            if (closeButton != null)
            {
                closeButton.gameObject.SetActive(showDialogParams.showCloseButton);
            }
        }

        public void Confirm()
        {
            hideCoroutine = StartCoroutine(Hide(confirmButtonClicked));
        }

        public void Close()
        {
            hideCoroutine = StartCoroutine(Hide(closeButtonClicked));
        }

        private IEnumerator Hide(Event eventToTrigger)
        {
            // The checking of the animation name is just to avoid this continuing on the first frame we transition from idle to closing
            while (animator != null && (animator.GetBool(CLOSED_ANIMATION_NAME) || animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f))
            {
                yield return null;
            }

            eventToTrigger.Raise();
            GameObject.Destroy(gameObject);
        }

        #endregion
    }
}

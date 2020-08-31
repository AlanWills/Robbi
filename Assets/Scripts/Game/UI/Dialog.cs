using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Events;
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
            public string description;
            public string confirmButtonText;
            public bool showConfirmButton;
            public bool showCloseButton;
            public Event confirmButtonPressed;
            public Event closeButtonPressed;
        }

        #region Properties and Fields

        [SerializeField]
        private Text title;

        [SerializeField]
        private Text description;

        [SerializeField]
        private Text confirmButtonText;

        [SerializeField]
        private Button confirmButton;

        [SerializeField]
        private Button closeButton;

        #endregion

        #region Show/Hide

        public void Show(ShowDialogParams showDialogParams)
        {
            if (title != null)
            {
                title.text = showDialogParams.title;
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
                    UnityEventTools.AddVoidPersistentListener(confirmButton.onClick, showDialogParams.confirmButtonPressed.Raise);
                    UnityEventTools.AddVoidPersistentListener(confirmButton.onClick, Hide);

                    if (confirmButtonText != null)
                    {
                        confirmButtonText.text = showDialogParams.confirmButtonText;
                    }
                }
            }

            if (closeButton != null)
            {
                closeButton.gameObject.SetActive(showDialogParams.showCloseButton);

                UnityEventTools.AddVoidPersistentListener(closeButton.onClick, showDialogParams.closeButtonPressed.Raise);
                UnityEventTools.AddVoidPersistentListener(confirmButton.onClick, Hide);
            }
        }

        public void Hide()
        {
            GameObject.Destroy(gameObject);
        }

        #endregion
    }
}

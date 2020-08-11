using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Viewport
{
    [AddComponentMenu("Robbi/Viewport/Drag")]
    [RequireComponent(typeof(Camera))]
    public class Drag : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private float scrollDelta = 1f;

        private bool mouseDownLastFrame = false;
        private Vector3 previousMouseDownPosition = new Vector3();
        private Camera cameraToDrag;

        #endregion

        #region Unity Methods

        private void Start()
        {
            cameraToDrag = GetComponent<Camera>();
        }

        private void Update()
        {
            bool isMouseDown = Input.GetMouseButton(1);
            if (isMouseDown)
            {
                Vector3 mousePosition = Input.mousePosition;

                if (mouseDownLastFrame)
                {
                    Vector3 mouseDelta = previousMouseDownPosition - mousePosition;
                    float scrollModifier = scrollDelta * Time.deltaTime * cameraToDrag.orthographicSize;

                    transform.Translate(mouseDelta.x * scrollModifier, mouseDelta.y * scrollModifier, 0);
                }
                
                previousMouseDownPosition = mousePosition;
            }

            mouseDownLastFrame = isMouseDown;
        }

        #endregion
    }
}

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

        private Camera cameraToDrag;

#if UNITY_ANDROID || UNITY_IPHONE
        private float timeSinceFingerDown = 0;
        private const float DRAG_THRESHOLD = 0.1f;
#else
        private bool mouseDownLastFrame = false;
        private Vector3 previousMouseDownPosition = new Vector3();
#endif

        #endregion

        #region Unity Methods

        private void Start()
        {
            cameraToDrag = GetComponent<Camera>();
        }

        private void Update()
        {
#if UNITY_ANDROID || UNITY_IPHONE
            if (Input.touchCount != 1)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    timeSinceFingerDown = 0;
                    break;

                case TouchPhase.Stationary:
                    timeSinceFingerDown += Time.deltaTime;
                    break;

                case TouchPhase.Moved:
                    timeSinceFingerDown += Time.deltaTime;

                    if (timeSinceFingerDown >= DRAG_THRESHOLD)
                    {
                        Vector2 dragAmount = -touch.deltaPosition;
                        float scrollModifier = scrollDelta * Time.deltaTime * cameraToDrag.orthographicSize;

                        transform.Translate(dragAmount.x * scrollModifier, dragAmount.y * scrollModifier, 0);
                    }
                    break;

                default:
                    timeSinceFingerDown = 0;
                    break;
            }
#else
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
#endif
        }

        #endregion
    }
}

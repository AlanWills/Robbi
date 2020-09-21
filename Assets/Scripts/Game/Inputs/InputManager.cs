using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Event = Robbi.Events.Event;

namespace Robbi.Game.Inputs
{
    [AddComponentMenu("Robbi/Input/Input Manager")]
    public class InputManager : MonoBehaviour
    {
        #region Properties and Fields

        public Camera raycastCamera;

        #region Desktop Events

        [Header("Desktop Events)")]
        public Vector3Event leftMouseButtonFirstDown;
        public Vector3Event leftMouseButtonDown;
        public Vector3Event leftMouseButtonFirstUp;

        public Vector3Event middleMouseButtonFirstDown;
        public Vector3Event middleMouseButtonDown;
        public Vector3Event middleMouseButtonFirstUp;

        public Vector3Event rightMouseButtonFirstDown;
        public Vector3Event rightMouseButtonDown;
        public Vector3Event rightMouseButtonFirstUp;

        public FloatEvent mouseScrolled;

        #endregion

        #region Phone Events

        [Header("Phone Events")]
        public TouchEvent singleTouchEvent;
        public MultiTouchEvent doubleTouchEvent;
        public MultiTouchEvent tripleTouchEvent;

        #endregion

        #region Common Events

        [Header("Common Events")]
        public GameObjectClickEvent gameObjectLeftClicked;

        #endregion

        #endregion

        #region Unity Methods

        public void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);
                singleTouchEvent.Raise(touch);

                Vector3 touchWorldPosition = raycastCamera.ScreenToWorldPoint(touch.position);
                GameObject hitGameObject = Raycast(new Vector2(touchWorldPosition.x, touchWorldPosition.y));

                if (hitGameObject != null && touch.phase == TouchPhase.Began)
                {
                    gameObjectLeftClicked.Raise(new GameObjectClickEventArgs() 
                    { 
                        gameObject = hitGameObject, 
                        clickWorldPosition = touchWorldPosition 
                    });
                }
            }
            else if (Input.touchCount == 2)
            {
                doubleTouchEvent.Raise(new MultiTouchEventArgs()
                {
                    touchCount = 2,
                    touches = Input.touches,
                });
            }
            else if (Input.touchCount == 3)
            {
                tripleTouchEvent.Raise(new MultiTouchEventArgs()
                {
                    touchCount = 3,
                    touches = Input.touches,
                });
            }
#else
            CheckMouseButton(0, leftMouseButtonFirstDown, leftMouseButtonDown, leftMouseButtonFirstUp);
            CheckMouseButton(1, rightMouseButtonFirstDown, rightMouseButtonDown, rightMouseButtonFirstUp);
            CheckMouseButton(2, middleMouseButtonFirstDown, middleMouseButtonDown, middleMouseButtonFirstUp);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mouseWorldPosition = raycastCamera.ScreenToWorldPoint(Input.mousePosition);
                GameObject hitGameObject = Raycast(new Vector2(mouseWorldPosition.x, mouseWorldPosition.y));

                if (hitGameObject != null)
                {
                    gameObjectLeftClicked.Raise(new GameObjectClickEventArgs()
                    {
                        gameObject = hitGameObject,
                        clickWorldPosition = mouseWorldPosition
                    });
                }
            }

            float mouseScrollDelta = Input.mouseScrollDelta.y;
            if (mouseScrollDelta != 0)
            {
                mouseScrolled.Raise(mouseScrollDelta);
            }
#endif
        }

        #endregion

        #region Utility Functions

        private void CheckMouseButton(
            int mouseButton, 
            Vector3Event mouseButtonFirstDown,
            Vector3Event mouseButtonDown,
            Vector3Event mouseButtonFirstUpEvent)
        {
            if (Input.GetMouseButtonDown(mouseButton))
            {
                mouseButtonFirstDown.Raise(Input.mousePosition);
            }

            if (Input.GetMouseButton(mouseButton))
            {
                mouseButtonDown.Raise(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(mouseButton))
            {
                mouseButtonFirstUpEvent.Raise(Input.mousePosition);
            }
        }

        #endregion

        #region Raycasting

        private GameObject Raycast(Vector2 origin)
        {
            RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.zero);
            return raycastHit.transform != null ? raycastHit.transform.gameObject : null;
        }

        #endregion
    }
}

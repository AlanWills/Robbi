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
        public Event leftMouseButtonDown;

        #endregion

        #region Phone Events

        [Header("Phone Events")]
        public TouchEvent singleTouchEvent;

        #endregion

        #region Common Events

        [Header("Common Events")]
        public GameObjectClickEvent gameObjectLeftClicked;

        #endregion

        #endregion

        #region Unity Methods

        // Have events fired in particular input situations
        // Also have a function for doing raycasting etc.
        // This will listen out for those events being fired and then do appropriate behaviour (e.g. raycasting)
        // Then we can simulate input
        // Finally, will have to have a component which can be notified it's been clicked or maybe an event where we passed the clicked game object
        // Can have a component which derives from the listener interface, but also does the check internally to see if it's this clicked game object
        // before Invoking on UnityEvent.  Will save boilerplate at listener sites everywhere (call it InputListener)

        public void Update()
        {
#if UNITY_ANDROID || UNITY_IOS
            if (Input.touchCount == 1)
            {
                singleTouchEvent.Raise(Input.GetTouch(0));

                Vector3 touchWorldPosition = raycastCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
                GameObject hitGameObject = Raycast(new Vector2(touchWorldPosition.x, touchWorldPosition.y));

                if (hitGameObject != null)
                {
                    gameObjectLeftClicked.Raise(new GameObjectClickEventArgs() 
                    { 
                        gameObjectClicked = hitGameObject, 
                        clickWorldPosition = touchWorldPosition 
                    });
                }
            }
#else
            if (Input.GetMouseButtonDown(0))
            {
                leftMouseButtonDown.Raise();

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
#endif
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

using Robbi.Events;
using Robbi.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Viewport
{
    [AddComponentMenu("Robbi/Viewport/Zoom")]
    [RequireComponent(typeof(Camera))]
    public class Zoom : MonoBehaviour
    {
        #region Properties and Fields

        private Camera cameraToZoom;
        private float minZoom = 20;
        private float maxZoom = 2;
        private float zoomSpeed = 1;

        #endregion

        #region Unity Methods

        private void Start()
        {
            cameraToZoom = GetComponent<Camera>();

            GameSettings gameSettings = GameSettings.Load();
            minZoom = gameSettings.MinZoom;
            maxZoom = gameSettings.MaxZoom;
            zoomSpeed = gameSettings.ZoomSpeed;
        }

        #endregion

        #region Zoom Utility Methods

        public void ZoomUsingScroll(float scrollAmount)
        {
            if (scrollAmount != 0)
            {
                cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize - scrollAmount, minZoom, maxZoom);
            }
        }

        public void ZoomUsingPinch(MultiTouchEventArgs touchEventArgs)
        {
            Debug.AssertFormat(touchEventArgs.touchCount == 2, "Expected 2 touches for ZoomUsingPinch, but got {0}", touchEventArgs.touchCount);
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize + deltaMagnitudeDiff * zoomSpeed, minZoom, maxZoom);
            }
        }

        #endregion
    }
}

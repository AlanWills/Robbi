using Robbi.Events;
using Robbi.Options;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Viewport
{
    [AddComponentMenu("Robbi/Viewport/Tilemap Zoom")]
    [RequireComponent(typeof(Camera))]
    public class TilemapZoom : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private TilemapValue tilemap;

        private Camera cameraToZoom;
        private float minZoom = 0.75f;
        private float maxZoom = 2;
        private float zoomSpeed = 1;

        #endregion

        #region Unity Methods

        private void Start()
        {
            cameraToZoom = GetComponent<Camera>();

            OptionsManager settingsManager = OptionsManager.Instance;
            minZoom = settingsManager.MinZoom;
            maxZoom = settingsManager.MaxZoom;
            zoomSpeed = settingsManager.ZoomSpeed;
        }

        #endregion

        #region Zoom Utility Methods

        public void ZoomUsingScroll(float scrollAmount)
        {
            if (scrollAmount != 0)
            {
                FitCamera(scrollAmount);
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
                float deltaMagnitudeDiff = touchDeltaMag - prevTouchDeltaMag;
                
                FitCamera(deltaMagnitudeDiff);
            }
        }
        
        private void FitCamera(float unscaledDeltaZoomAmount)
        {
            // Zoom out
            cameraToZoom.orthographicSize -= unscaledDeltaZoomAmount * zoomSpeed;

            Bounds bounds = tilemap.value.localBounds;
            Vector3 bottomLeft = cameraToZoom.WorldToViewportPoint(bounds.min);
            Vector3 topRight = cameraToZoom.WorldToViewportPoint(bounds.max);
            float difference = Math.Max(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

            if (difference < minZoom)
            {
                // Reached zoom out limit
                cameraToZoom.orthographicSize *= difference / minZoom;
            }
            else if (difference > maxZoom)
            {
                // Reached zoom in limit
                cameraToZoom.orthographicSize *= difference / maxZoom;
            }
        }

        public void FullZoomOut()
        {
            Bounds bounds = tilemap.value.localBounds;
            Vector3 bottomLeft = cameraToZoom.WorldToViewportPoint(bounds.min);
            Vector3 topRight = cameraToZoom.WorldToViewportPoint(bounds.max);
            float difference = Math.Max(topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

            cameraToZoom.orthographicSize *= difference / minZoom;
        }

        #endregion
    }
}

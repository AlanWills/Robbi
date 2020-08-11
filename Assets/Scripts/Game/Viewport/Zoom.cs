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

        public float minZoom = 20;
        public float maxZoom = 2;
        
        private Camera cameraToZoom;

        #endregion

        #region Unity Methods

        private void Start()
        {
            cameraToZoom = GetComponent<Camera>();
        }

        private void Update()
        {
            float mouseScrollDelta = Input.mouseScrollDelta.y;
            if (mouseScrollDelta != 0)
            {
                cameraToZoom.orthographicSize = Mathf.Clamp(cameraToZoom.orthographicSize - mouseScrollDelta, minZoom, maxZoom);
            }
        }

        #endregion
    }
}

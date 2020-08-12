using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Movement Debug")]
    public class MovementDebug : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private bool debugEnabled = true;

        private TilemapRenderer tilemapRenderer;

        #endregion

        #region Unity Methods

        private void Start()
        {
            tilemapRenderer = GetComponent<TilemapRenderer>();

            if (Application.isPlaying)
            {
                tilemapRenderer.enabled = debugEnabled;
            }
        }

        #endregion
    }
}

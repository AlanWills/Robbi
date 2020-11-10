using Robbi.Parameters;
using Robbi.Viewport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels
{
    [AddComponentMenu("Robbi/Levels/Level Root")]
    public class LevelRoot : MonoBehaviour
    {
        #region Properties and Fields

        [Header("Tilemaps")]
        public Tilemap corridorsTilemap;
        public Tilemap destructibleCorridorsTilemap;
        public Tilemap exitsTilemap;
        public Tilemap doorsTilemap;
        public Tilemap interactablesTilemap;
        public Tilemap movementTilemap;

        [Header("Parameters")]
        public TilemapValue corridorsTilemapValue;
        public TilemapValue destructibleCorridorsTilemapValue;
        public TilemapValue exitsTilemapValue;
        public TilemapValue doorsTilemapValue;
        public TilemapValue interactablesTilemapValue;
        public TilemapValue movementTilemapValue;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            corridorsTilemapValue.value = corridorsTilemap;
            destructibleCorridorsTilemapValue.value = destructibleCorridorsTilemap;
            exitsTilemapValue.value = exitsTilemap;
            doorsTilemapValue.value = doorsTilemap;
            interactablesTilemapValue.value = interactablesTilemap;
            movementTilemapValue.value = movementTilemap;

            // Not great, but a hacky workaround for the fact the camera can't start zoomed out
            // Maybe in the future this is resolved by a level started event or something
            Camera.main.GetComponent<TilemapZoom>().FullZoomOut();
        }

        #endregion

        #region Editor Only Functions

#if UNITY_EDITOR
        public void EditorOnly_CompressAllTilemaps()
        {
            corridorsTilemap.CompressBounds();
            destructibleCorridorsTilemap.CompressBounds();
            exitsTilemap.CompressBounds();
            doorsTilemap.CompressBounds();
            interactablesTilemap.CompressBounds();
            movementTilemap.CompressBounds();
        }
#endif

#endregion
    }
}

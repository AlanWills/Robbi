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
        public Tilemap collectablesTilemap;
        public Tilemap movementTilemap;

        [Header("Parameters")]
        public TilemapValue corridorsTilemapValue;
        public TilemapValue destructibleCorridorsTilemapValue;
        public TilemapValue exitsTilemapValue;
        public TilemapValue doorsTilemapValue;
        public TilemapValue interactablesTilemapValue;
        public TilemapValue collectablesTilemapValue;
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
            collectablesTilemapValue.value = collectablesTilemap;
            movementTilemapValue.value = movementTilemap;

            // Not great, but a hacky workaround for the fact the camera can't start zoomed out and centred
            // Maybe in the future this is resolved by a level started event or something
            Camera.main.GetComponent<TilemapZoom>().FullZoomOut();
            Bounds bounds = corridorsTilemap.localBounds;
            Camera.main.transform.position = new Vector3(bounds.center.x, bounds.center.y, Camera.main.transform.position.z);
        }

        #endregion

        #region Editor Only Functions

#if UNITY_EDITOR
        public void EditorOnly_CompressAllTilemaps()
        {
            EditorOnly_CompressBounds(corridorsTilemap);
            EditorOnly_CompressBounds(destructibleCorridorsTilemap);
            EditorOnly_CompressBounds(exitsTilemap);
            EditorOnly_CompressBounds(doorsTilemap);
            EditorOnly_CompressBounds(interactablesTilemap);
            EditorOnly_CompressBounds(collectablesTilemap);
            EditorOnly_CompressBounds(movementTilemap);
        }

        private void EditorOnly_CompressBounds(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;
            tilemap.CompressBounds();

            if (bounds != tilemap.cellBounds)
            {
                UnityEditor.EditorUtility.SetDirty(tilemap);
                UnityEditor.EditorUtility.SetDirty(tilemap.gameObject);
            }
        }
#endif

#endregion
    }
}

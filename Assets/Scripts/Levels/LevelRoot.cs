using Celeste.Tilemaps;
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
        public Tilemap portalsTilemap;
        public Tilemap exitsTilemap;
        public Tilemap doorsTilemap;
        public Tilemap interactablesTilemap;
        public Tilemap collectablesTilemap;
        public Tilemap movementTilemap;

        [Header("Parameters")]
        [SerializeField] private TilemapValue corridorsTilemapValue;
        [SerializeField] private TilemapValue destructibleCorridorsTilemapValue;
        [SerializeField] public TilemapValue portalsTilemapValue;
        [SerializeField] private TilemapValue exitsTilemapValue;
        [SerializeField] private TilemapValue doorsTilemapValue;
        [SerializeField] private TilemapValue interactablesTilemapValue;
        [SerializeField] private TilemapValue collectablesTilemapValue;
        [SerializeField] private TilemapValue movementTilemapValue;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            corridorsTilemapValue.Value = corridorsTilemap;
            destructibleCorridorsTilemapValue.Value = destructibleCorridorsTilemap;
            portalsTilemapValue.Value = portalsTilemap;
            exitsTilemapValue.Value = exitsTilemap;
            doorsTilemapValue.Value = doorsTilemap;
            interactablesTilemapValue.Value = interactablesTilemap;
            collectablesTilemapValue.Value = collectablesTilemap;
            movementTilemapValue.Value = movementTilemap;

            // Not great, but a hacky workaround for the fact the camera can't start zoomed out and centred
            // Maybe in the future this is resolved by a level started event or something
            Bounds bounds = corridorsTilemap.localBounds;
            Camera.main.transform.position = new Vector3(bounds.center.x, bounds.center.y, Camera.main.transform.position.z);
            Camera.main.GetComponent<TilemapZoom>().FullZoomOut();
        }

        #endregion

        #region Editor Only Functions

#if UNITY_EDITOR
        public void EditorOnly_CompressAllTilemaps()
        {
            EditorOnly_CompressBounds(corridorsTilemap);
            EditorOnly_CompressBounds(destructibleCorridorsTilemap);
            EditorOnly_CompressBounds(portalsTilemap);
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

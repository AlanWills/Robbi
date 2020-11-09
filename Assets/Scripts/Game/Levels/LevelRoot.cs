using Robbi.Parameters;
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
        }

        #endregion
    }
}

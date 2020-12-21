using Celeste.FSM;
using Celeste.Tilemaps;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Set Tile")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class SetTileNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public Vector3Int tilePosition;
        public TilemapValue tilemap;
        public Tile newTile;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            Vector3Int _tilePosition = GetInputValue(nameof(tilePosition), tilePosition);
            Debug.AssertFormat(newTile != null || tilemap.Value.HasTile(_tilePosition), 
                "NewTile null: {0} Has Tile: {1} Position: {2}", 
                newTile == null, 
                tilemap.Value.HasTile(_tilePosition), 
                _tilePosition);
            tilemap.Value.SetTile(_tilePosition, newTile);
        }

        #endregion
    }
}

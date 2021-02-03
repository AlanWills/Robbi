using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Runtime.Actors
{
    public abstract class CharacterRuntime : MonoBehaviour
    {
        #region Properties and Fields

        public abstract Vector3 Position { get; set; }

        public Vector3Int Tile
        {
            get 
            {
                Vector3 position = Position;
                return new Vector3Int(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y), 0);
            }
        }

        #endregion

        #region Environment Runtime

        public abstract void OnHitByLaser();

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    public struct InteractArgs
    {
        public Tilemap interactablesTilemap;
    }

    public interface IInteractable
    {
        #region Properties and Fields

        Vector3Int Position { get; }
        TileBase InteractedTile { get; }

        #endregion

        void Initialize();
        void Interact(InteractArgs interactArgs);
    }
}

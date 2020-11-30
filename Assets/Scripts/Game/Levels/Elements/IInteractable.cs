﻿using System;
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
        Tile InteractedTile { get; }
        Tile UninteractedTile { get; }

        #endregion

        void Interact(InteractArgs interactArgs);
    }
}
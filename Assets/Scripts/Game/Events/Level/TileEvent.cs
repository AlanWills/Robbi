﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace Robbi.Events
{
    [Serializable]
    public class TileUnityEvent : UnityEvent<Tile> { }

    [Serializable]
    [CreateAssetMenu(fileName = "TileEvent", menuName = "Robbi/Events/Tile Event")]
    public class TileEvent : ParameterisedEvent<Tile>
    {
    }
}

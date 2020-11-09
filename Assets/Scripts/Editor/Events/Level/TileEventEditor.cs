using Robbi.Events;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace RobbiEditor.Events
{
    [CustomEditor(typeof(TileEvent))]
    public class TileEventEditor : ParameterisedEventEditor<Tile, TileEvent>
    {
        protected override Tile DrawArgument(Tile argument)
        {
            return EditorGUILayout.ObjectField(argument, typeof(Tile), false) as Tile;
        }
    }
}

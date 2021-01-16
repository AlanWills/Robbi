using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Tilemaps.WaveFunctionCollapse
{
    [Serializable]
    public enum Direction
    {
        LeftOf,
        Above,
        RightOf,
        Below
    }

    public static class DirectionExtensions
    {
        public static Direction Opposite(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Above:
                    return Direction.Below;

                case Direction.Below:
                    return Direction.Above;

                case Direction.LeftOf:
                    return Direction.RightOf;

                case Direction.RightOf:
                    return Direction.LeftOf;

                default:
                    Debug.LogErrorFormat("Unhandled Direction: {0}", direction);
                    return Direction.LeftOf;
            }
        }
    }
}

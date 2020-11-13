using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RobbiEditor.Constants
{
    public enum DoorColour
    {
        Green,
        Red,
        Blue,
        Grey
    }

    public static class DoorColours
    {
        public static readonly Color GREEN = new Color(41.0f / 255, 1.0f, 0);
        public static readonly Color RED = new Color(1, 0, 41.0f / 255);
        public static readonly Color BLUE = new Color(0, 140.0f / 255, 1);
        public static readonly Color GREY = new Color(160.0f / 255, 160.0f / 255, 160.0f / 255);

        public static readonly Color[] COLOURS = new Color[]
        {
            GREEN,
            RED,
            BLUE,
            GREY,
        };

        public static readonly string[] COLOUR_NAMES = new string[]
        {
            "Green",
            "Red",
            "Blue",
            "Grey",
        };
    }
}

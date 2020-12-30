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
        Brown
    }

    public static class DoorColours
    {
        public static readonly Color GREEN = new Color(0, 210.0f / 255.0f, 0.0f);
        public static readonly Color RED = new Color(214.0f / 255.0f, 40.0f / 255.0f, 0.0f);
        public static readonly Color BLUE = new Color(0, 128.0f / 255.0f, 176.0f / 255.0f);
        public static readonly Color BROWN = new Color(200.0f, 120.0f, 0.0f);

        public static readonly Color[] COLOURS = new Color[]
        {
            GREEN,
            RED,
            BLUE,
            BROWN
        };

        public static readonly string[] COLOUR_NAMES = new string[]
        {
            "Green",
            "Red",
            "Blue",
            "Brown"
        };
    }
}

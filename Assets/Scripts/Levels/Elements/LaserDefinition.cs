using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "New LaserDefinition", menuName = "Robbi/Levels/Laser Definition")]
    public class LaserDefinition : ScriptableObject
    {
        [Header("Active")]
        public Tile startActiveTile;
        public Tile middleActiveTile;
        public Tile endActiveTile;

        [Header("Inactive")]
        public Tile startInactiveTile;
        public Tile middleInactiveTile;
        public Tile endInactiveTile;
    }
}

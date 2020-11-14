using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Tilemaps;

namespace Robbi.Levels.Modifiers
{
    public class ToggleDoorModifier : LevelModifier
    {
        #region Properties and Fields

        public Door door;

        #endregion

        public override void Execute(Tilemap doorTilemap)
        {
            door.Toggle(doorTilemap);
        }
    }
}

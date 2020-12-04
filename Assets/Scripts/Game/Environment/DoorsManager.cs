using Robbi.Levels.Elements;
using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Environment
{
    [AddComponentMenu("Robbi/Environment/Doors Manager")]
    public class DoorsManager : NamedManager
    {
        #region Properties and Fields

        public TilemapValue doorsTilemap;

        #endregion

        #region Doors Methods

        public void OpenDoor(Door door)
        {
            door.Open(doorsTilemap.value);
        }

        public void CloseDoor(Door door)
        {
            door.Close(doorsTilemap.value);
        }

        public void ToggleDoor(Door door)
        {
            door.Toggle(doorsTilemap.value);
        }

        #endregion
    }
}

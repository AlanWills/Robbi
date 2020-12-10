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

        private List<Door> doors = new List<Door>();

        #endregion

        #region Doors Management

        public void SetDoors(IEnumerable<Door> _doors)
        {
            doors.Clear();
            doors.AddRange(_doors);

            foreach (Door door in doors)
            {
                door.Initialize(doorsTilemap.Value);
            }
        }

        #endregion

        #region Doors Methods

        public void OpenDoor(Door door)
        {
            door.Open(doorsTilemap.Value);
        }

        public void CloseDoor(Door door)
        {
            door.Close(doorsTilemap.Value);
        }

        public void ToggleDoor(Door door)
        {
            door.Toggle(doorsTilemap.Value);
        }

        #endregion
    }
}

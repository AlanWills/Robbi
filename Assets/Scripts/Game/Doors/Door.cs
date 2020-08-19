using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Doors
{
    [CreateAssetMenu(fileName = "Door", menuName = "Robbi/Obstacles/Door")]
    public class Door : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int position;
        public Vector3IntEvent onDoorOpened;

        #endregion

        #region Door Management

        public void Open()
        {
            onDoorOpened.Raise(position);
        }

        #endregion
    }
}

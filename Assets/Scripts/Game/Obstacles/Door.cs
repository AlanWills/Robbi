using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Obstacles
{
    [CreateAssetMenu(fileName = "Door", menuName = "Robbi/Obstacles/Door")]
    public class Door : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int position;
        public Vector3IntGameEvent onDoorOpened;

        #endregion

        #region Door Management

        public void Open()
        {
            onDoorOpened.Raise(position);
        }

        #endregion
    }
}

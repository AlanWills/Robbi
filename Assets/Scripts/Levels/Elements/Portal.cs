using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Levels.Elements
{
    [CreateAssetMenu(fileName = "New Portal", menuName = "Robbi/Levels/Portal")]
    public class Portal : ScriptableObject
    {
        #region Properties and Fields

        public Vector3Int entrance1;
        public Vector3Int entrance2;

        #endregion

        public bool IsAtEntrance(Vector3Int location)
        {
            return location == entrance1 || location == entrance2;
        }

        public Vector3Int GetExit(Vector3Int entrance)
        {
            if (entrance == entrance1)
            {
                return entrance2;
            }
            else if (entrance == entrance2)
            {
                return entrance1;
            }

            Debug.LogAssertionFormat("Inputted {0} is not a valid Portal Entrance", entrance);
            return entrance;
        }
    }
}

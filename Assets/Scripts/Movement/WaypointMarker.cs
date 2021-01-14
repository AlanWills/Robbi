using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Robbi.Movement
{
    [AddComponentMenu("Robbi/Movement/Waypoint Marker")]
    public class WaypointMarker : MonoBehaviour
    {
        #region Properties and Fields

        [SerializeField]
        private TextMeshPro waypointNumber;

        #endregion

        public void SetWaypointNumber(int waypointIndex)
        {
            waypointNumber.text = waypointIndex.ToString();
        }
    }
}

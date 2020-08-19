using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [Serializable]
    public class Vector3IntUnityEvent : UnityEvent<Vector3Int> { }

    [CreateAssetMenu(fileName = "Vector3IntEvent", menuName = "Robbi/Events/Vector3Int Event")]
    public class Vector3IntEvent : AbstractEvent<Vector3Int> { }
}

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

    [CreateAssetMenu(fileName = "Vector3IntGameEvent", menuName = "Robbi/Events/Vector3Int Game Event")]
    public class Vector3IntGameEvent : AbstractGameEvent<Vector3Int> { }
}

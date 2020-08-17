using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Vector3Int Game Event Listener")]
    public class Vector3IntGameEventListener : AbstractGameEventListener<Vector3Int, Vector3IntGameEvent, Vector3IntUnityEvent>
    {
    }
}

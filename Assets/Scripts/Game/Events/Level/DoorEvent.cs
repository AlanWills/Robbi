using Robbi.Levels.Elements;
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
    public class DoorUnityEvent : UnityEvent<Door> { }

    [Serializable]
    [CreateAssetMenu(fileName = "DoorEvent", menuName = "Robbi/Events/Door Event")]
    public class DoorEvent : ParameterisedEvent<Door>
    {
    }
}

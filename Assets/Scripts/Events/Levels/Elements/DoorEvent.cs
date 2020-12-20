using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events.Levels.Elements
{
    [Serializable]
    public class DoorUnityEvent : UnityEvent<Door> { }

    [Serializable]
    [CreateAssetMenu(fileName = "DoorEvent", menuName = "Robbi/Events/Levels/Door Event")]
    public class DoorEvent : ParameterisedEvent<Door>
    {
    }
}

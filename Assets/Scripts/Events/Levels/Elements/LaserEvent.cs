using Celeste.Events;
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
    public class LaserUnityEvent : UnityEvent<Laser> { }

    [Serializable]
    [CreateAssetMenu(fileName = "New LaserEvent", menuName = "Robbi/Events/Levels/Laser Event")]
    public class LaserEvent : ParameterisedEvent<Laser>
    {
    }
}

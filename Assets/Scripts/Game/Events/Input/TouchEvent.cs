﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [Serializable]
    public class TouchUnityEvent : UnityEvent<Touch> { }

    [Serializable]
    [CreateAssetMenu(fileName = "TouchEvent", menuName = "Robbi/Events/Touch Event")]
    public class TouchEvent : ParameterisedEvent<Touch>
    {
    }
}

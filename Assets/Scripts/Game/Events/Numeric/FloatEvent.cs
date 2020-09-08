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
    public class FloatUnityEvent : UnityEvent<float> { }

    [Serializable]
    [CreateAssetMenu(fileName = "FloatEvent", menuName = "Robbi/Events/Float Event")]
    public class FloatEvent : ParameterisedEvent<float>
    {
    }
}

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
    public struct MultiTouchEventArgs
    {
        public int touchCount;
        public Touch[] touches;
    }

    [Serializable]
    public class MultiTouchUnityEvent : UnityEvent<MultiTouchEventArgs> { }

    [Serializable]
    [CreateAssetMenu(fileName = "MultiTouchEvent", menuName = "Robbi/Events/Multi Touch Event")]
    public class MultiTouchEvent : ParameterisedEvent<MultiTouchEventArgs>
    {
    }
}

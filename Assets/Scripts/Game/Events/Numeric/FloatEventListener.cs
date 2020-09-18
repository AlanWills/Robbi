﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Float Event Listener")]
    public class FloatEventListener : ParameterisedEventListener<float, FloatEvent, FloatUnityEvent>
    {
    }
}
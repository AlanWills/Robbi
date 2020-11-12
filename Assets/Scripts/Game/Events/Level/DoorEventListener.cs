﻿using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Door Event Listener")]
    public class DoorEventListener : ParameterisedEventListener<Door, DoorEvent, DoorUnityEvent>
    {
    }
}

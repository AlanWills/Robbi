﻿using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Interactable Event Listener")]
    public class InteractableEventListener : ParameterisedEventListener<Interactable, InteractableEvent, InteractableUnityEvent>
    {
    }
}

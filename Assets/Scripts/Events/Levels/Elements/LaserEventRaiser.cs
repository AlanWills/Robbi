﻿using Celeste.Events;
using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events.Levels.Elements
{
    [AddComponentMenu("Robbi/Events/Levels/Laser Event Raiser")]
    public class LaserEventRaiser : ParameterisedEventRaiser<Laser, LaserEvent>
    {
    }
}

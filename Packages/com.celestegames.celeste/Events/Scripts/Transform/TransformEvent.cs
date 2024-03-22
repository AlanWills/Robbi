﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Celeste.Events
{
    [Serializable]
    public class TransformUnityEvent : UnityEvent<Transform> { }

    [Serializable]
    [CreateAssetMenu(fileName = "TransformEvent", menuName = CelesteMenuItemConstants.EVENTS_MENU_ITEM + "Transform Event", order = CelesteMenuItemConstants.EVENTS_MENU_ITEM_PRIORITY)]
    public class TransformEvent : ParameterisedEvent<Transform>
    {
    }
}

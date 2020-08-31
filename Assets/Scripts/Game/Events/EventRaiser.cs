﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Event Raiser")]
    public class EventRaiser : MonoBehaviour, IEventRaiser
    {
        #region Properties and Fields

        public Event gameEvent;

        #endregion

        #region Response Methods

        public void Raise()
        {
            gameEvent.Raise();
        }

        #endregion
    }
}
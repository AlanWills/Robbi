﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Event Listener")]
    public class EventListener : MonoBehaviour, IEventListener
    {
        #region Properties and Fields

        public Event gameEvent;
        public UnityEvent response;

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            gameEvent.AddEventListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveEventListener(this);
        }

        #endregion

        #region Response Methods

        public void OnEventRaised()
        {
            response.Invoke();
        }

        #endregion
    }
}

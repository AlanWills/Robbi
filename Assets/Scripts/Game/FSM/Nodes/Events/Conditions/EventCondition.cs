using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    public abstract class EventCondition : ScriptableObject
    {
        #region Properties and Fields

        private bool hasEventFired = false;

        public abstract new string name { get; }

        #endregion

        #region Listeners

        public void AddListener()
        {
            hasEventFired = false;
            AddListenerInternal();
        }

        public void RemoveListener()
        {
            RemoveListenerInternal();
        }

        protected abstract void AddListenerInternal();

        protected abstract void RemoveListenerInternal();

        #endregion

        #region Event Firing

        public bool HasEventFired()
        {
            return hasEventFired;
        }

        public void OnEventRaised()
        {
            hasEventFired = true;
        }

        #endregion
    }
}

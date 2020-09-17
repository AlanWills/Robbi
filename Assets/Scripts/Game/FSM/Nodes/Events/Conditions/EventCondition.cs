using Robbi.Events;
using Robbi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    public abstract class EventCondition : ScriptableObject, ICopyable<EventCondition>
    {
        #region Properties and Fields

        private bool hasEventBeenRaised = false;

        public abstract new string name { get; }

        public object Argument { get; protected set; }

        #endregion

        #region Listeners

        public void AddListener()
        {
            hasEventBeenRaised = false;
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
            return hasEventBeenRaised;
        }

        public void RegisterEventRaised(object argument)
        {
            hasEventBeenRaised = true;
            Argument = argument;
        }

        #endregion

        #region ICopyable

        public abstract void CopyFrom(EventCondition original);

        #endregion
    }
}

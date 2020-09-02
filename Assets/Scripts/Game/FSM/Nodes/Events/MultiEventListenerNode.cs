using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static XNode.Node;

using Event = Robbi.Events.Event;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    public abstract class EventCondition : ScriptableObject
    {
        #region Properties and Fields

        private bool hasEventFired = false;

        #endregion

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

        public bool HasEventFired()
        {
            return hasEventFired;
        }

        public void OnEventRaised()
        {
            hasEventFired = true;
        }
    }

    [Serializable]
    public class VoidEventCondition : EventCondition, IEventListener
    {
        #region Properties and Fields

        public Event listenFor;

        public new string name
        {
            get { return listenFor != null ? listenFor.name : ""; }
        }

        #endregion

        #region IEventCondition

        protected override void AddListenerInternal()
        {
            listenFor.AddEventListener(this);
        }

        protected override void RemoveListenerInternal()
        {
            listenFor.RemoveEventListener(this);
        }

        #endregion
    }

    [Serializable]
    [CreateNodeMenu("Robbi/Events/Multi Event Listener Node")]
    public class MultiEventListenerNode : FSMNode
    {
        #region Properties and Fields

        [SerializeField]
        private List<EventCondition> events = new List<EventCondition>();

        public IEnumerable<EventCondition> Events
        {
            get { return events; }
        }

        #endregion

        #region Event Condition Utilities

        public T AddEvent<T>() where T : EventCondition
        {
            return AddEvent(typeof(T)) as T;
        }

        public EventCondition AddEvent(Type conditionType)
        {
            EventCondition _event = ScriptableObject.CreateInstance(conditionType) as EventCondition;
            events.Add(_event);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.AddObjectToAsset(graph, _event);
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(graph);
#endif

            return _event;
        }

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            foreach (EventCondition eventCondition in events)
            {
                eventCondition.AddListener();
            }
        }

        protected override FSMNode OnUpdate()
        {
            foreach (EventCondition eventCondition in events)
            {
                if (eventCondition.HasEventFired())
                {
                    return GetConnectedNode(eventCondition.name);
                }
            }

            return null;
        }

        protected override void OnExit()
        {
            base.OnExit();

            foreach (EventCondition eventCondition in events)
            {
                eventCondition.RemoveListener();
            }
        }

        #endregion
    }
}

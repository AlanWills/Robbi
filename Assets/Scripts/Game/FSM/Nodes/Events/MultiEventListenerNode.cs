using Robbi.Events;
using Robbi.FSM.Nodes.Events.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;
using static XNode.Node;

using Event = Robbi.Events.Event;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Multi Event Listener Node")]
    public class MultiEventListenerNode : FSMNode
    {
        #region Properties and Fields

        [SerializeField]
        private List<EventCondition> events = new List<EventCondition>();

        public uint NumEvents
        {
            get { return (uint)events.Count; }
        }

        #endregion

        public MultiEventListenerNode()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);
        }

        #region Event Condition Utilities

        public EventCondition GetEvent(uint index)
        {
            return index < NumEvents ? events[(int)index] : null;
        }

        public T AddEvent<T>() where T : EventCondition
        {
            return AddEvent(typeof(T)) as T;
        }

        public EventCondition AddEvent(Type conditionType)
        {
            EventCondition _event = ScriptableObject.CreateInstance(conditionType) as EventCondition;
            events.Add(_event);

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.AddObjectToAsset(_event, this);
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(graph);
#endif

            return _event;
        }

        public void RemoveEvent(EventCondition eventCondition)
        {
            events.Remove(eventCondition);

            bool hasPort = HasPort(eventCondition.name);
            Debug.Assert(hasPort, string.Format("Missing port {0} for event condition being removed.", eventCondition.name));

            if (hasPort)
            {
                RemoveDynamicPort(eventCondition.name);
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(eventCondition);
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(graph);
#endif
        }

        public void AddEventConditionPort(string name)
        {
            AddOutputPort(name);
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
                    string eventConditionName = eventCondition.name;
                    Debug.Log("Name: " + eventConditionName);
                    return GetConnectedNode(eventConditionName);
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

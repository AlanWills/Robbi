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
    [CreateNodeMenu("Robbi/Events/Listeners/Multi Event Listener")]
    [NodeTint(0.8f, 0.9f, 0)]
    public class MultiEventListenerNode : FSMNode
    {
        #region Properties and Fields

        [Output]
        public object argument;

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

        #region Add/Remove/Copy

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            MultiEventListenerNode originalListenerNode = original as MultiEventListenerNode;

            for (uint i = 0; i < originalListenerNode.NumEvents; ++i)
            {
                EventCondition originalCondition = originalListenerNode.GetEvent(i);
                EventCondition newCondition = AddEvent(originalCondition.GetType());
                newCondition.CopyFrom(originalCondition);
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            for (uint i = NumEvents; i > 0; --i)
            {
                RemoveEvent(GetEvent(i - 1));
            }
        }

        #endregion

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
            UnityEditor.AssetDatabase.AddObjectToAsset(_event, graph);
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(graph);
#endif

            return _event;
        }

        public void RemoveEvent(EventCondition eventCondition)
        {
            bool hasPort = HasPort(eventCondition.name);
            Debug.Assert(hasPort, string.Format("Missing port {0} for event condition being removed.", eventCondition.name));

            if (hasPort)
            {
                RemoveDynamicPort(eventCondition.name);
            }

            events.Remove(eventCondition);

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

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            return argument;
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
            FSMNode connectedNode = null;

            foreach (EventCondition eventCondition in events)
            {
                if (eventCondition.HasEventFired())
                {
                    if (connectedNode == null)
                    {
                        string eventConditionName = eventCondition.name;
                        argument = eventCondition.ConsumeEvent();

                        Debug.LogFormat("Name: {0} with Argument: {1} was consumed by MEL Node", eventConditionName, argument != null ? argument : "");
                        connectedNode = GetConnectedNode(eventConditionName);
                    }
                    else
                    {
                        eventCondition.ConsumeEvent();
                    }
                }
            }

            return connectedNode != null ? connectedNode : this;
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

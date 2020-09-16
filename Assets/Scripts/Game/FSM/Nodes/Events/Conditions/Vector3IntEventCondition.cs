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
    public class Vector3IntEventCondition : EventCondition, IEventListener<Vector3Int>
    {
        #region Properties and Fields

        public Vector3IntEvent listenFor;

        public override string name
        {
            get { return listenFor != null ? listenFor.name : ""; }
        }

        #endregion

        #region Listeners

        protected override void AddListenerInternal()
        {
            listenFor.AddEventListener(this);
        }

        protected override void RemoveListenerInternal()
        {
            listenFor.RemoveEventListener(this);
        }

        public void OnEventRaised(Vector3Int arg)
        {
            OnEventRaised();
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(EventCondition original)
        {
            Vector3IntEventCondition eventCondition = original as Vector3IntEventCondition;
            eventCondition.listenFor = listenFor;
        }

        #endregion
    }
}

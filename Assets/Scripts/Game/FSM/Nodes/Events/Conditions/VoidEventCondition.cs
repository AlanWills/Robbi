using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Events.Conditions
{
    [Serializable]
    public class VoidEventCondition : EventCondition, IEventListener
    {
        #region Properties and Fields

        public Event listenFor;

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

        #endregion

        #region ICopyable

        public override void CopyFrom(EventCondition original)
        {
            VoidEventCondition voidEventCondition = original as VoidEventCondition;
            voidEventCondition.listenFor = listenFor;
        }

        #endregion
    }
}

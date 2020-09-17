﻿using Robbi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robbi.FSM.Nodes.Events
{
    [Serializable]
    [CreateNodeMenu("Robbi/Events/Event Raiser")]
    public class EventRaiserNode : FSMNode
    {
        #region Properties and Fields

        public Event toRaise;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            toRaise.Raise();
        }

        #endregion
    }
}

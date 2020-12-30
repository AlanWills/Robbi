using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.FSM.Nodes
{
    public enum WaitUnit
    {
        Seconds,
        Frames
    }

    [Serializable]
    [CreateNodeMenu("Celeste/Flow/Wait")]
    [NodeTint(0, 0.4f, 0)]
    public class WaitNode : FSMNode
    {
        #region Properties and Fields

        public float time = 1;
        public WaitUnit unit = WaitUnit.Seconds;

        private float currentTime = 0;

        #endregion

        #region FSM Runtime Methods

        protected override void OnEnter()
        {
            base.OnEnter();

            currentTime = 0;
        }

        protected override FSMNode OnUpdate()
        {
            if (unit == WaitUnit.Seconds)
            {
                currentTime += Time.deltaTime;
            }
            else if (unit == WaitUnit.Frames)
            {
                ++currentTime;
            }
            else
            {
                Debug.LogAssertionFormat("Unhandled WaitUnit {0} in WaitNode in graph {1}", unit, graph.name);
            }

            return currentTime >= time ? base.OnUpdate() : this;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Wait Node")]
    public class WaitNode : FSMNode
    {
        #region Properties and Fields

        public float time = 1;

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
            currentTime += Time.deltaTime;

            return currentTime >= time ? base.OnUpdate() : null;
        }

        #endregion
    }
}

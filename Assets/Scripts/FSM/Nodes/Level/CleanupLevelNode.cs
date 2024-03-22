using System;
using UnityEngine;
using Celeste.FSM;
using Celeste.Parameters;

namespace Robbi.FSM.Nodes
{
    [Serializable]
    [CreateNodeMenu("Robbi/Level/Cleanup Level")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class CleanupLevelNode : FSMNode
    {
        #region Properties and Fields

        [Header("Parameters")]
        public GameObjectValue levelGameObject;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject.Destroy(levelGameObject.Value);
        }

        #endregion
    }
}

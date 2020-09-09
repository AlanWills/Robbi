using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Loading
{
    [CreateNodeMenu("Robbi/Loading/Instantiate Prefab")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class InstantiatePrefabNode : FSMNode
    {
        #region Properties and Fields

        public GameObject prefab;
        public Transform parent;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject.Instantiate(prefab, parent);
        }

        #endregion
    }
}

using Robbi.FSM.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.FSM
{
    [AddComponentMenu("Robbi/FSM/FSM Runtime")]
    public class FSMRuntime : SceneGraph<FSMGraph>
    {
        #region Properties and Fields

        public FSMNode CurrentNode { get; private set; }

        private FSMNode nodeToTransitionTo;

        #endregion

        #region Unity Methods

        private void Start()
        {
            nodeToTransitionTo = graph.startNode;
        }

        private void Update()
        {
            if (nodeToTransitionTo != null)
            {
                if (CurrentNode != null)
                {
                    CurrentNode.Exit();
                }

                CurrentNode = nodeToTransitionTo;
                CurrentNode.Enter();

                nodeToTransitionTo = null;
            }

            if (CurrentNode != null)
            {
                nodeToTransitionTo = CurrentNode.Update();
            }
        }

        #endregion
    }
}

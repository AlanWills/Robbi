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
            CurrentNode = graph.startNode;
            CurrentNode.Enter();
        }

        private void Update()
        {
            FSMNode newNode = null;

            while (CurrentNode != newNode)
            {
                newNode = CurrentNode.Update();

                if (newNode != CurrentNode)
                {
                    CurrentNode.Exit();
                    CurrentNode = newNode;
                    CurrentNode.Enter();
                }
            }
        }

        #endregion
    }
}

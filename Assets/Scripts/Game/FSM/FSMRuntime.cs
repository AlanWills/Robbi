﻿using Robbi.FSM.Nodes;
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
            Debug.Assert(graph.nodes.Count == 0 || graph.startNode != null, "FSMRuntime graph contains nodes, but no start node so will be disabled.");
            CurrentNode = graph.startNode;

            if (CurrentNode != null)
            {
                Debug.LogFormat("Spooling up FSM with starting node {0}", CurrentNode.name);
                CurrentNode.Enter();
            }
            else 
            {
                enabled = false;
            }
        }

        private void Update()
        {
            if (CurrentNode == null)
            {
                return;
            }

            FSMNode newNode = CurrentNode.Update();

            while (newNode != CurrentNode)
            {
                CurrentNode.Exit();
                CurrentNode = newNode;

                if (CurrentNode != null)
                {
                    CurrentNode.Enter();
                    newNode = CurrentNode.Update();
                }
            }
        }

        #endregion
    }
}

﻿using Robbi.Events;
using Robbi.Game.Inputs;
using Robbi.Hierarchy;
using Robbi.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Robbi.FSM.Nodes.Flow
{
    [CreateNodeMenu("Robbi/Flow/Wait For Scene")]
    public class WaitForSceneNode : FSMNode
    {
        #region Properties and Fields

        public string sceneName;
        public float attemptWindow = 1;

        private float currentTime = 0;

        private const string FOUND_OUTPUT_PORT = "Found";
        private const string NOT_FOUND_OUTPUT_PORT = "NotFound";

        #endregion

        public WaitForSceneNode()
        {
            RemoveDynamicPort(DEFAULT_OUTPUT_PORT_NAME);

            AddOutputPort(FOUND_OUTPUT_PORT);
            AddOutputPort(NOT_FOUND_OUTPUT_PORT);
        }

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            currentTime = 0;
        }

        protected override FSMNode OnUpdate()
        {
            if (currentTime <= attemptWindow)
            {
                currentTime += Time.deltaTime;

                Scene scene = SceneManager.GetSceneByName(sceneName);
                if (scene != null && scene.IsValid())
                {
                    Debug.LogFormat("Found scene {0}", sceneName);
                    return GetConnectedNode(FOUND_OUTPUT_PORT);
                }
                else
                {
                    // We are still within the attempt window so stay within this node
                    return this;
                }
            }

            Debug.LogFormat("Could not find Scene with name {0}", sceneName);
            return GetConnectedNode(NOT_FOUND_OUTPUT_PORT);
        }

        #endregion
    }
}

using Robbi.Events;
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
using UnityEngine.UI;

namespace Robbi.FSM.Nodes.Input
{
    [CreateNodeMenu("Robbi/Input/Click GameObject")]
    public class ClickGameObjectNode : FSMNode
    {
        #region Properties and Fields

        public GameObjectPath gameObjectPath = new GameObjectPath();
        public float attemptWindow = 1;

        private float currentTime = 0;

        #endregion

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

                GameObject gameObject = gameObjectPath.GameObject;
                if (gameObject != null)
                {
                    gameObject.Click();
                    return base.OnUpdate();
                }
                else
                {
                    // We are still within the attempt window so stay within this node
                    return this;
                }
            }

            Debug.LogFormat("Could not find GameObject with path {0}", gameObjectPath);
            return base.OnUpdate();
        }

        #endregion
    }
}

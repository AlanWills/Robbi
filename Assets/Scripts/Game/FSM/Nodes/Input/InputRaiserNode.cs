using Robbi.Events;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Input
{
    [Serializable]
    [CreateNodeMenu("Robbi/Input/Input Raiser")]
    [NodeWidth(250)]
    public class InputRaiserNode : FSMNode
    {
        #region Properties and Fields

        public GameObjectClickEvent inputEvent;
        public Vector3 inputPosition;
        public string gameObjectName;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            GameObject gameObject = GameObject.Find(gameObjectName);
            Debug.AssertFormat(gameObject != null, "GameObject {0} could not be found in InputRaiserNode", gameObjectName);
            inputEvent.Raise(new GameObjectClickEventArgs() { clickWorldPosition = inputPosition, gameObject = gameObject });
        }

        #endregion
    }
}

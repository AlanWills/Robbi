using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Viewport
{
    public enum LookAxis
    {
        X,
        Y,
        Z
    }

    [Serializable]
    [CreateNodeMenu("Robbi/Camera/Look At")]
    [NodeWidth(250)]
    public class LookAtNode : FSMNode
    {
        #region Properties and Fields

        public Vector3Reference targetPosition;
        public LookAxis lookAxis;

        #endregion

        #region Add/Remove/Copy

        protected override void OnAddToGraph()
        {
            base.OnAddToGraph();

            if (targetPosition == null)
            {
                targetPosition = CreateParameter<Vector3Reference>(name + "_targetPosition");
            }
        }

        protected override void OnRemoveFromGraph()
        {
            base.OnRemoveFromGraph();

            RemoveParameter(targetPosition);
        }

        protected override void OnCopyInGraph(FSMNode original)
        {
            base.OnCopyInGraph(original);

            LookAtNode lookAtNode = original as LookAtNode;
            targetPosition = CreateParameter(lookAtNode.targetPosition);
        }

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 position = targetPosition.Value;

            if (lookAxis == LookAxis.X)
            {
                position.x = cameraPosition.x;
            }
            else if (lookAxis == LookAxis.Y)
            {
                position.y = cameraPosition.y;
            }
            else if (lookAxis == LookAxis.Z)
            {
                position.z = cameraPosition.z;
            }

            Camera.main.transform.position = position;
        }

        #endregion
    }
}

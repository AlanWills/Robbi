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
    [CreateAssetMenu(fileName = "FSMGraph", menuName = "Robbi/FSM/FSM Graph")]
    public class FSMGraph : NodeGraph
    {
        #region Properties and Fields

        public FSMNode startNode;

        #endregion

        #region Node Utility Methods

        public override Node AddNode(Type type)
        {
            FSMNode node = base.AddNode(type) as FSMNode;
            startNode = startNode == null ? node : startNode;
            node.AddToGraph();

            return node;
        }

        public override void RemoveNode(Node node)
        {
            FSMNode fsmNode = node as FSMNode;
            fsmNode.RemoveFromGraph();

            base.RemoveNode(node);
        }

        public override Node CopyNode(Node original)
        {
            FSMNode copy = base.CopyNode(original) as FSMNode;
            copy.CopyInGraph(original as FSMNode);

            return copy;
        }

        #endregion
    }
}

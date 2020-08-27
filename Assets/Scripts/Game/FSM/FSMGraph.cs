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
            Node node = base.AddNode(type);

            if (startNode == null)
            {
                startNode = node as FSMNode;
            }

            return node;
        }

        #endregion
    }
}

using Robbi.DataSystem.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.DataSystem
{
    [CreateAssetMenu(fileName = "DataGraph", menuName = "Robbi/Data/Data Graph")]
    public class DataGraph : NodeGraph
    {
        #region Properties and Fields

        public GameObject GameObject
        {
            set
            {
                foreach (Node node in nodes)
                {
                    if (node is INeedsGameObject)
                    {
                        (node as INeedsGameObject).GameObject = value;
                    }
                }
            }
        }

        #endregion

        #region Data Updating

        public void Update()
        {
            foreach (Node node in nodes)
            {
                if (node is IUpdateable)
                {
                    (node as IUpdateable).Update();
                }
            }
        }

        #endregion
    }
}

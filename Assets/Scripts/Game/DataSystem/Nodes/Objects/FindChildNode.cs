using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Robbi.DataSystem.Nodes.Objects
{
    [Serializable]
    [CreateNodeMenu("Robbi/Objects/Find Child")]
    public class FindChildNode : DataNode, IRequiresGameObject
    {
        #region Properties and Fields

        public GameObject GameObject { get; set; }

        [Input]
        public string childName;

        public bool cache = true;

        [Output]
        public GameObject child;

        private GameObject foundChild;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            if (!cache || foundChild == null)
            {
                string _childName = GetInputValue("childName", childName);
                foundChild = GameObject.transform.Find(_childName).gameObject;
            }

            return foundChild;
        }

        #endregion
    }
}

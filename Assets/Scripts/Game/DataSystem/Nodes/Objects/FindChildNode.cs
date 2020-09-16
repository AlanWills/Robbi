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
    public class FindChildNode : DataNode
    {
        #region Properties and Fields

        [Input]
        public string childName;

        [Output]
        public GameObject child;

        private GameObject gameObject;
        private GameObject foundChild;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            if (foundChild == null)
            {
                string _childName = GetInputValue("childName", childName);
                foundChild = gameObject.transform.Find(_childName).gameObject;
            }

            return foundChild;
        }

        #endregion
    }
}

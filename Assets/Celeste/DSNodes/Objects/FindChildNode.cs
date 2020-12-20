using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

namespace Celeste.DS.Nodes.Objects
{
    [Serializable]
    [CreateNodeMenu("Celeste/Objects/Find Child")]
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
                string _childName = GetInputValue(nameof(childName), childName);
                Transform childTransform = GameObject.transform.Find(_childName);
                foundChild = childTransform == null ? GameObject.Find(_childName) : childTransform.gameObject;
            }

            Debug.AssertFormat(foundChild != null, "Could not find child {0}", childName);
            return foundChild;
        }

        #endregion
    }
}

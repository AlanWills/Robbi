using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Robbi.DataSystem.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Robbi/UI/Image")]
    public class ImageNode : DataNode, IUpdateable, INeedsGameObject
    {
        #region Properties and Fields

        public GameObject GameObject
        {
            set { image = value.GetComponent<Image>(); }
        }

        [Input]
        public bool isEnabled;

        private Image image;

        #endregion

        #region IUpdateable

        public void Update()
        {
            image.enabled = GetInputValue<bool>("isEnabled");
        }

        #endregion
    }
}

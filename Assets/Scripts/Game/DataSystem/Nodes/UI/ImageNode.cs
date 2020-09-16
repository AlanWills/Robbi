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
    public class ImageNode : DataNode, IUpdateable
    {
        #region Properties and Fields

        [Input]
        public bool isEnabled;

        public Image image;

        #endregion

        #region IUpdateable

        public void Update()
        {
            image.enabled = GetInputValue("isEnabled", isEnabled);
        }

        #endregion
    }
}

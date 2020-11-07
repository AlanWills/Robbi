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

        [Input]
        public Sprite sprite;

        public Image image;

        #endregion

        #region IUpdateable

        public void Update()
        {
            image.enabled = GetInputValue(nameof(isEnabled), isEnabled);

            // Don't allow setting null sprites - use the isEnabled field instead
            Sprite _sprite = GetInputValue(nameof(sprite), sprite);
            if (_sprite != null && image.sprite != _sprite)
            {
                image.sprite = _sprite;
            }
        }

        #endregion
    }
}

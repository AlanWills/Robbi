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
    [CreateNodeMenu("Robbi/UI/Text")]
    public class TextNode : DataNode, IUpdateable, INeedsGameObject
    {
        #region Properties and Fields

        public GameObject GameObject
        {
            set { text = value.GetComponent<Text>(); }
        }

        [Input]
        public string value;

        public string format;

        private Text text;
        private string oldValue;

        #endregion

        public void Update()
        {
            string currentValue = GetInputValue("value", "");
            if (currentValue != oldValue)
            {
                text.GetComponent<Text>().text = string.Format(format, currentValue);
                oldValue = currentValue;
            }
        }
    }
}

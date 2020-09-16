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
    public class TextNode : DataNode, IUpdateable
    {
        #region Properties and Fields

        [Input]
        public string value;

        [Input]
        public string format;

        public Text text;

        private string oldValue;

        #endregion

        public void Update()
        {
            string currentValue = GetInputValue("value", value);
            if (currentValue != oldValue)
            {
                string _format = GetInputValue("format", format);
                text.GetComponent<Text>().text = string.Format(_format, currentValue);
                oldValue = currentValue;
            }
        }
    }
}

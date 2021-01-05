﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XNode;

namespace Celeste.DS.Nodes.UI
{
    [Serializable]
    [CreateNodeMenu("Celeste/UI/Text")]
    public class TextNode : DataNode, IUpdateable
    {
        #region Properties and Fields

        [Input]
        public string value;

        [Input]
        public string format;

        [Input]
        public Text text;

        private string oldValue;

        #endregion

        #region Node Runtime

        protected override void Init()
        {
            base.Init();

            oldValue = "";
        }

        #endregion

        #region IUpdateable

        public void Update()
        {
            string currentValue = GetInputValue("value", value);
            if (currentValue != oldValue)
            {
                string _format = GetInputValue("format", format);
                GetText().text = string.IsNullOrEmpty(_format) ? currentValue : string.Format(_format, currentValue);
                oldValue = currentValue;
            }
        }

        #endregion

        #region Utility Methods

        private Text GetText()
        {
            Text _text = GetInputValue(nameof(text), text);
            if (_text == null)
            {
                GameObject gameObject = GetInputValue<GameObject>(nameof(text));
                if (gameObject != null)
                {
                    _text = gameObject.GetComponent<Text>();
                }
            }

            Debug.AssertFormat(_text != null, "Could not find Text component in {0} in {1}", name, graph.name);
            return _text;
        }

        #endregion
    }
}

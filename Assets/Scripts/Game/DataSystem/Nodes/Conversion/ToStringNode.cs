﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.DataSystem.Nodes.Conversion
{
    [Serializable]
    public class ToStringNode<T> : DataNode
    {
        #region Properties and Fields

        [Input]
        public T input;

        [Input]
        public string format = "{0}";

        [Output]
        public string output;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            T _input = GetInputValue("input", input);
            string _format = GetInputValue("format", format);
            return string.Format(_format, _input);
        }

        #endregion
    }
}
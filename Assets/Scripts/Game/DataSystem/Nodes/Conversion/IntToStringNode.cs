﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.DataSystem.Nodes.Conversion
{
    [CreateNodeMenu("Robbi/Conversion/Int To String")]
    public class IntToStringNode : DataNode
    {
        #region Properties and Fields

        [Input]
        public int input;

        [Input]
        public string format;

        [Output]
        public string output;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            int _input = GetInputValue("input", input);
            string _format = GetInputValue("format", format);
            return _input.ToString(_format);
        }

        #endregion
    }
}
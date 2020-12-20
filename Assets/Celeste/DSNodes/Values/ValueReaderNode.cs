using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.DataSystem.Nodes.Values
{
    [Serializable]
    public abstract class ValueReaderNode<T, TValue> : DataNode where TValue : ParameterValue<T>
    {
        #region Properties and Fields

        [Input]
        public TValue value;

        [Output]
        public T output;

        #endregion

        #region Node Overrides

        public override object GetValue(NodePort port)
        {
            return GetInputValue(nameof(value), value).Value;
        }

        #endregion
    }
}

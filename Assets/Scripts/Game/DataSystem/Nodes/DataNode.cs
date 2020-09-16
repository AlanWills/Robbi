using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNode;

namespace Robbi.DataSystem.Nodes
{
    [Serializable]
    public abstract class DataNode : Node
    {
        #region Add/Remove/Copy

        public void AddToGraph()
        {
            OnAddToGraph();
        }

        protected virtual void OnAddToGraph() { }

        public void RemoveFromGraph()
        {
            OnRemoveFromGraph();
        }

        protected virtual void OnRemoveFromGraph() { }

        public void CopyInGraph(DataNode original)
        {
            OnCopyInGraph(original);
        }

        protected virtual void OnCopyInGraph(DataNode original) { }

        #endregion

    }
}

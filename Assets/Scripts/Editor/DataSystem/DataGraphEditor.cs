using Robbi.DataSystem;
using Robbi.DataSystem.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using XNodeEditor;

namespace RobbiEditor.DataSystem
{
    [CustomNodeGraphEditor(typeof(DataGraph))]
    public class DataGraphEditor : NodeGraphEditor
    {
        #region Context Menu

        public override string GetNodeMenuName(Type type)
        {
            return typeof(DataNode).IsAssignableFrom(type) ? base.GetNodeMenuName(type) : null;
        }

        #endregion
    }
}

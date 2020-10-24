using Robbi.FSM.Nodes;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;

namespace RobbiEditor.FSM.Nodes.Level
{
    [CustomNodeEditor(typeof(OperateDoorNode))]
    public class OperateDoorNodeEditor : FSMNodeEditor
    {
        #region Unity Methods

        public override void OnCreate()
        {
            base.OnCreate();

            OperateDoorNode doorNode = target as OperateDoorNode;
            if (doorNode.doorsTilemap == null)
            {
                doorNode.doorsTilemap = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.DOORS_TILEMAP_DATA);
            }
        }

        #endregion
    }
}

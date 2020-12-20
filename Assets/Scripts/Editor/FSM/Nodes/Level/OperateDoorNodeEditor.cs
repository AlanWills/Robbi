using Robbi.FSM.Nodes;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using Celeste.Viewport;

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
                doorNode.doorsTilemap = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.DOORS_TILEMAP);
            }
        }

        #endregion
    }
}

using Robbi.FSM.Nodes;
using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using Celeste.Tilemaps;
using CelesteEditor.FSM.Nodes;

namespace RobbiEditor.FSM.Nodes.Level
{
    [CustomNodeEditor(typeof(OperateInteractableNode))]
    public class OperateInteractableNodeEditor : FSMNodeEditor
    {
        #region Unity Methods

        public override void OnCreate()
        {
            base.OnCreate();

            OperateInteractableNode interactableNode = target as OperateInteractableNode;
            if (interactableNode.interactablesTilemap == null)
            {
                interactableNode.interactablesTilemap = AssetDatabase.LoadAssetAtPath<TilemapValue>(ParameterFiles.INTERACTABLES_TILEMAP);
            }      
        }

        #endregion
    }
}

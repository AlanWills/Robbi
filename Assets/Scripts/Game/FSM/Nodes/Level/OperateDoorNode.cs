using Robbi.Levels.Elements;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes
{
    public enum DoorOperation
    {
        Open,
        Close,
        Toggle
    }

    [Serializable]
    [CreateNodeMenu("Robbi/Level/Operate Door")]
    [NodeTint(0.2f, 0.2f, 0.6f)]
    public class OperateDoorNode : FSMNode
    {
        #region Properties and Fields

        [Input]
        public Door door;
        public TilemapValue doorsTilemap;
        public DoorOperation operation;

        #endregion

        #region FSM Runtime

        protected override void OnEnter()
        {
            base.OnEnter();

            Door _door = GetInputValue(nameof(door), door);
            
            if (operation == DoorOperation.Open)
            {
                _door.Open(doorsTilemap.Value);
            }
            else if (operation == DoorOperation.Close)
            {
                _door.Close(doorsTilemap.Value);
            }
            else if (operation == DoorOperation.Toggle)
            {
                _door.Toggle(doorsTilemap.Value);
            }
            else
            {
                Debug.LogAssertion("Unhandled door operation");
            }
        }

        #endregion
    }
}

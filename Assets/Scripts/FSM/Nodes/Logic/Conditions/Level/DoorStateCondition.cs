using Celeste.Logic;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using Robbi.Tilemaps.Tiles;
using System.ComponentModel;
using UnityEngine;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("Door State")]
    public class DoorStateCondition : Condition
    {
        #region Properties and Fields

        public Door door;
        public TilemapValue doorsTilemap;
        public ConditionOperator condition;
        public DoorState doorState;

        #endregion

        #region Check Methods

        protected override bool DoCheck()
        {
            DoorTile doorTile = doorsTilemap.Value.GetTile<DoorTile>(door.position);
            if (doorTile == null)
            {
                Debug.LogAssertionFormat("No Door found at {0}", door.position);
                return false;
            }

            switch (condition)
            {
                case ConditionOperator.Equals:
                    return doorTile.GetDoorState(door.position) == doorState;

                case ConditionOperator.NotEquals:
                    return doorTile.GetDoorState(door.position) != doorState;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in {1}", condition, GetType().Name);
                    return false;
            }
        }

        public override void SetVariable(object arg)
        {
            Debug.LogAssertion($"{nameof(SetVariable)} is not supported in condition {name}.");
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(Condition original)
        {
            DoorStateCondition doorStateCondition = original as DoorStateCondition;
            door = doorStateCondition.door;
            doorsTilemap = doorStateCondition.doorsTilemap;
            condition = doorStateCondition.condition;
            doorState = doorStateCondition.doorState;
        }

        #endregion
    }
}

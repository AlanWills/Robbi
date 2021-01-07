using Celeste.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;
using Celeste.Parameters;
using Celeste.Tilemaps;
using Robbi.Levels.Elements;
using Robbi.Tilemaps.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    public class DoorStateCondition : Condition
    {
        #region Properties and Fields

        public Door door;
        public TilemapValue doorsTilemap;
        public ConditionOperator condition;
        public DoorState doorState;

        #endregion

        #region Init Methods

#if UNITY_EDITOR
        public override void Init_EditorOnly(IParameterContainer parameterContainer) { }

        public override void Cleanup_EditorOnly(IParameterContainer parameterContainer) { }
#endif

        #endregion

        #region Check Methods

        public sealed override bool Check(object arg)
        {
            return Check();
        }

        private bool Check()
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

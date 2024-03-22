using Celeste.Parameters;
using UnityEngine;
using Celeste.Logic;
using Celeste.Tilemaps;
using System.ComponentModel;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("In Tilemap")]
    public class InTilemapCondition : Condition
    {
        #region Properties and Fields

        public TilemapValue value;
        public ConditionOperator condition;
        public Vector3IntReference target;

        #endregion

        #region Init Methods

        protected override void DoInitialize()
        {
            base.DoInitialize();

            if (target == null)
            {
                target = CreateInstance<Vector3IntReference>();
                target.name = name + "_value";
#if UNITY_EDITOR
                UnityEditor.AssetDatabase.AddObjectToAsset(target, this);
#endif
            }
        }

        #endregion

        #region Check Methods

        public override void SetVariable(object arg)
        {
            target.IsConstant = true;
            target.Value = arg != null ? (Vector3Int)arg : default;
        }

        protected override bool DoCheck()
        {
            switch (condition)
            {
                case ConditionOperator.Equals:
                    return value.Value.HasTile(target.Value);

                case ConditionOperator.NotEquals:
                    return !value.Value.HasTile(target.Value);

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in InTilemap Condition", condition);
                    return false;
            }
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(Condition original)
        {
            InTilemapCondition valueCondition = original as InTilemapCondition;
            value = valueCondition.value;
            condition = valueCondition.condition;
            target.CopyFrom(valueCondition.target);
        }

        #endregion
    }
}

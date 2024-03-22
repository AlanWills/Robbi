using Robbi.Levels.Elements;
using Celeste.Parameters;
using UnityEngine;
using Celeste.Logic;
using System.ComponentModel;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("At Collectable")]
    public class AtCollectableCondition : Condition
    {
        #region Properties and Fields

        public Collectable value;
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
                target.name = $"{name}_target";
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.AddObjectToAsset(target, this);
#endif
            }
        }

        #endregion

        #region Check Methods

        protected override bool DoCheck()
        {
            switch (condition)
            {
                case ConditionOperator.Equals:
                    return value.Position == target.Value;

                case ConditionOperator.NotEquals:
                    return value.Position != target.Value;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in AtCollectable Condition", condition);
                    return false;
            }
        }

        public override void SetVariable(object arg)
        {
            target.IsConstant = true;
            target.Value = arg != null ? (Vector3Int)arg : default;
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(Condition original)
        {
            AtCollectableCondition atCollectableCondition = original as AtCollectableCondition;
            value = atCollectableCondition.value;
            condition = atCollectableCondition.condition;
            target.CopyFrom(atCollectableCondition.target);
        }

        #endregion
    }
}

using Robbi.Levels.Elements;
using Celeste.Parameters;
using UnityEngine;
using Celeste.Logic;
using System.ComponentModel;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("At Interactable")]
    public class AtInteractableCondition : Condition
    {
        #region Properties and Fields

        public Interactable value;
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
                    return value.Position == target.Value;

                case ConditionOperator.NotEquals:
                    return value.Position != target.Value;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in AtSwitch Condition", condition);
                    return false;
            }
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(Condition original)
        {
            AtInteractableCondition atSwitchCondition = original as AtInteractableCondition;
            value = atSwitchCondition.value;
            condition = atSwitchCondition.condition;
            target.CopyFrom(atSwitchCondition.target);
        }

        #endregion
    }
}

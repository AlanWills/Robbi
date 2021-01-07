using Robbi.Levels.Elements;
using Celeste.Parameters;
using UnityEngine;
using Celeste.FSM.Nodes.Logic.Conditions;
using Celeste.Logic;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    public class AtInteractableCondition : Condition
    {
        #region Properties and Fields

        public bool useArgument = false;
        public Interactable value;
        public ConditionOperator condition;
        public Vector3IntReference target;

        #endregion

        #region Init Methods

#if UNITY_EDITOR
        public override void Init_EditorOnly(IParameterContainer parameterContainer)
        {
            if (target == null)
            {
                target = parameterContainer.CreateParameter<Vector3IntReference>(name + "_target");
            }
        }

        public override void Cleanup_EditorOnly(IParameterContainer parameterContainer)
        {
            if (target != null)
            {
                parameterContainer.RemoveAsset(target);
            }
        }
#endif

        #endregion

        #region Check Methods

        public sealed override bool Check(object arg)
        {
            if (useArgument)
            {
                target.IsConstant = true;
                target.Value = arg != null ? (Vector3Int)arg : default;
            }

            return Check();
        }

        private bool Check()
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
            useArgument = atSwitchCondition.useArgument;
            value = atSwitchCondition.value;
            condition = atSwitchCondition.condition;
            target.CopyFrom(atSwitchCondition.target);
        }

        #endregion
    }
}

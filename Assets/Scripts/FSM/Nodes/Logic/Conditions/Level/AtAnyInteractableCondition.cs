using Robbi.Levels.Elements;
using Celeste.Parameters;
using System.Collections.Generic;
using UnityEngine;
using Celeste.Logic;
using System.ComponentModel;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    [DisplayName("At Any Interactable")]
    public class AtAnyInteractableCondition : Condition
    {
        #region Properties and Fields

        public List<Interactable> value = new List<Interactable>();
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
                UnityEditor.EditorUtility.SetDirty(this);
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
                    foreach (Interactable interactable in value)
                    {
                        if (interactable.Position == target.Value)
                        {
                            return true;
                        }
                    }

                    return false;

                case ConditionOperator.NotEquals:
                    foreach (Interactable interactable in value)
                    {
                        if (interactable.Position == target.Value)
                        {
                            return false;
                        }
                    };

                    return true;

                default:
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in AtAnySwitch Condition", condition);
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
            AtAnyInteractableCondition atAnySwitchCondition = original as AtAnyInteractableCondition;
            value.AddRange(atAnySwitchCondition.value);
            condition = atAnySwitchCondition.condition;
            target.CopyFrom(atAnySwitchCondition.target);
        }

        #endregion
    }
}

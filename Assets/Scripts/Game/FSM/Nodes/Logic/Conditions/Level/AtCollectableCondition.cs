﻿using Robbi.Levels.Elements;
using Robbi.Logic;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.FSM.Nodes.Logic.Conditions
{
    public class AtCollectableCondition : ValueCondition
    {
        #region Properties and Fields

        public bool useArgument = false;
        public Collectable value;
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
                    Debug.LogAssertionFormat("Condition Operator {0} is not supported in AtCollectable Condition", condition);
                    return false;
            }
        }

        #endregion

        #region ICopyable

        public override void CopyFrom(ValueCondition original)
        {
            AtCollectableCondition atCollectableCondition = original as AtCollectableCondition;
            useArgument = atCollectableCondition.useArgument;
            value = atCollectableCondition.value;
            condition = atCollectableCondition.condition;
            target.CopyFrom(atCollectableCondition.target);
        }

        #endregion
    }
}
using Robbi.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [Serializable]
    public class ParameterReference<T, TValue, TReference> : ScriptableObject, ICopyable<TReference> 
                where TValue : ParameterValue<T>
                where TReference : ParameterReference<T, TValue, TReference>
    {
        #region Properties and Fields

        public bool isConstant = true;

        [SerializeField]
        private T constantValue;

        [SerializeField]
        private TValue referenceValue;

        public T Value
        {
            get { return isConstant ? constantValue : referenceValue.value; }
            set
            {
                if (isConstant)
                {
                    constantValue = value;
                }
                else
                {
                    referenceValue.value = value;
                }
            }
        }

        #endregion

        #region Copy Methods

        public void CopyFrom(TReference otherParameter)
        {
            isConstant = otherParameter.isConstant;
            constantValue = otherParameter.constantValue;
            referenceValue = otherParameter.referenceValue;
        }

        #endregion
    }
}

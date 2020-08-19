using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    public class ParameterReference<T, TValue> : ScriptableObject where TValue : ParameterValue<T>
    {
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
    }
}

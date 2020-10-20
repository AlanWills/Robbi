using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [Serializable]
    public class ParameterValue<T> : ScriptableObject
    {
        [NonSerialized]
        public T value;
    }
}

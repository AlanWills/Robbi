using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    public class ParameterValue<T> : ScriptableObject
    {
        public T value;
    }
}

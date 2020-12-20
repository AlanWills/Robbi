using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "StringValue", menuName = "Celeste/Parameters/String/String Value")]
    public class StringValue : ParameterValue<string>
    {
    }
}

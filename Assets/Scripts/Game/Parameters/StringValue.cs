using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "StringValue", menuName = "Robbi/Parameters/String Value")]
    public class StringValue : ParameterValue<string>
    {
    }
}

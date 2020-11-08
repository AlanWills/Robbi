using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "StringReference", menuName = "Robbi/Parameters/String Reference")]
    public class StringReference : ParameterReference<string, StringValue, StringReference>
    {
    }
}

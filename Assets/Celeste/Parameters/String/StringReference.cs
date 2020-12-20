using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [Serializable]
    [CreateAssetMenu(fileName = "StringReference", menuName = "Celeste/Parameters/String/String Reference")]
    public class StringReference : ParameterReference<string, StringValue, StringReference>
    {
    }
}

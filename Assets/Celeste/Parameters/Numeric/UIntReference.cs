using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "UIntReference", menuName = "Celeste/Parameters/Numeric/UInt Reference")]
    public class UIntReference : ParameterReference<uint, UIntValue, UIntReference>
    {
    }
}

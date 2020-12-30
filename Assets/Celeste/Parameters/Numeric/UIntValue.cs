using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "UIntValue", menuName = "Celeste/Parameters/Numeric/UInt Value")]
    public class UIntValue : ParameterValue<uint>
    {
    }
}

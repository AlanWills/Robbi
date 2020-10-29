using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "UIntReference", menuName = "Robbi/Parameters/UInt Reference")]
    public class UIntReference : ParameterReference<uint, UIntValue, UIntReference>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "BoolReference", menuName = "Robbi/Parameters/Bool Reference")]
    public class BoolReference : ParameterReference<bool, BoolValue, BoolReference>
    {
    }
}

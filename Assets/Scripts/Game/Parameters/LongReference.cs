using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "LongReference", menuName = "Robbi/Parameters/Long Reference")]
    public class LongReference : ParameterReference<long, LongValue, LongReference>
    {
    }
}

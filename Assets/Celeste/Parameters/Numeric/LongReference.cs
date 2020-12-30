using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "LongReference", menuName = "Celeste/Parameters/Numeric/Long Reference")]
    public class LongReference : ParameterReference<long, LongValue, LongReference>
    {
    }
}

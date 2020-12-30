using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "IntReference", menuName = "Celeste/Parameters/Numeric/Int Reference")]
    public class IntReference : ParameterReference<int, IntValue, IntReference>
    {
    }
}

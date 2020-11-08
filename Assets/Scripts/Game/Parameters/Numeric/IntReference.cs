using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "IntReference", menuName = "Robbi/Parameters/Int Reference")]
    public class IntReference : ParameterReference<int, IntValue, IntReference>
    {
    }
}

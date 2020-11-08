using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "FloatReference", menuName = "Robbi/Parameters/Numeric/Float Reference")]
    public class FloatReference : ParameterReference<float, FloatValue, FloatReference>
    {
    }
}

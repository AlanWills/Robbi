﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "BoolReference", menuName = "Celeste/Parameters/Bool/Bool Reference")]
    public class BoolReference : ParameterReference<bool, BoolValue, BoolReference>
    {
    }
}

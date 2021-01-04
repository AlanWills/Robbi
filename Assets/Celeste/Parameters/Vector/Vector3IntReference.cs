﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "Vector3IntReference", menuName = "Celeste/Parameters/Vector/Vector3Int Reference")]
    public class Vector3IntReference : ParameterReference<Vector3Int, Vector3IntValue, Vector3IntReference>
    {
    }
}
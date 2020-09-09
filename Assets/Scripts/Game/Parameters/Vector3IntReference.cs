using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "Vector3IntReference", menuName = "Robbi/Parameters/Vector3Int Reference")]
    public class Vector3IntReference : ParameterReference<Vector3Int, Vector3IntValue, Vector3IntReference>
    {
    }
}
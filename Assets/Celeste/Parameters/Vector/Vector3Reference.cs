using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Celeste.Parameters
{
    [CreateAssetMenu(fileName = "Vector3Reference", menuName = "Celeste/Parameters/Vector/Vector3 Reference")]
    public class Vector3Reference : ParameterReference<Vector3, Vector3Value, Vector3Reference>
    {
    }
}
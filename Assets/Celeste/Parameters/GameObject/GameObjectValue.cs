using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "GameObjectValue", menuName = "Robbi/Parameters/Game Object/GameObject Value")]
    public class GameObjectValue : ParameterValue<GameObject>
    {
    }
}

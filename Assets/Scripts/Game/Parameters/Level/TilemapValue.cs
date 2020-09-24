using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Parameters
{
    [CreateAssetMenu(fileName = "TilemapValue", menuName = "Robbi/Parameters/Level/Tilemap Value")]
    public class TilemapValue : ParameterValue<Tilemap>
    {
    }
}

using Robbi.Levels.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Robbi.Runtime.Actors
{
    [AddComponentMenu("Robbi/Runtime/Actors/Enemy Runtime")]
    public class EnemyRuntime : CharacterRuntime
    {
        #region Lifetime

        public void Shutdown()
        {
            GameObject.Destroy(gameObject);
        }

        #endregion
    }
}

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
        #region Properties and Fields

        public override Vector3 Position
        {
            get { return transform.localPosition; }
            set { transform.localPosition = value; }
        }

        #endregion

        #region Environment Runtime

        public override void OnHitByLaser()
        {
            Kill();
        }

        #endregion

        #region Lifetime

        private void Kill()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}

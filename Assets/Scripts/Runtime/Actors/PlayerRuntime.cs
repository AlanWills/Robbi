using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Runtime.Actors
{
    [AddComponentMenu("Robbi/Runtime/Actors/Player Runtime")]
    public class PlayerRuntime : CharacterRuntime
    {
        #region Properties and Fields

        public Vector3Value localPosition;

        #endregion

        #region Unity Methods

        private void Update()
        {
            transform.localPosition = localPosition.Value;
        }

        #endregion
    }
}

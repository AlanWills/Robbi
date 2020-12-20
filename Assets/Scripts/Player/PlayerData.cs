using Celeste.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Player
{
    [AddComponentMenu("Robbi/Player/Player Data")]
    public class PlayerData : MonoBehaviour
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

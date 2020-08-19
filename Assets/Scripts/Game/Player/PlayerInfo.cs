using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Player
{
    [AddComponentMenu("Robbi/Player/Player Info")]
    public class PlayerInfo : MonoBehaviour
    {
        #region Properties and Fields

        public Vector3Value localPosition;

        #endregion

        #region Unity Methods

        private void Start()
        {
            localPosition.value = transform.localPosition;
        }

        private void Update()
        {
            transform.localPosition = localPosition.value;
        }

        #endregion
    }
}

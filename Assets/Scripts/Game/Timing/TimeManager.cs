using Robbi.Managers;
using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.Timing
{
    [AddComponentMenu("Robbi/Time/Time Manager")]
    public class TimeManager : NamedManager
    {
        #region Properties and Fields

        public FloatValue timeTaken;

        #endregion

        #region Unity Methods

        private void Start()
        {
            timeTaken.value = 0;
        }

        private void Update()
        {
            timeTaken.value += Time.deltaTime;
        }

        #endregion
    }
}

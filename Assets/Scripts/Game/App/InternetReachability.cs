using Robbi.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Robbi.App
{
    [AddComponentMenu("Robbi/App/InternetReachability")]
    public class InternetReachability : MonoBehaviour
    {
        #region Properties and Fields

        public BoolValue hasInternetConnection;

        #endregion

        #region Unity Methods

        private void Update()
        {
            hasInternetConnection.value = Application.internetReachability != NetworkReachability.NotReachable;
        }

        #endregion
    }
}

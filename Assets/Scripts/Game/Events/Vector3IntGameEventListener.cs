using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    [AddComponentMenu("Robbi/Events/Vector3Int Game Event Listener")]
    public class Vector3IntGameEventListener : MonoBehaviour
    {
        #region Properties and Fields

        public Vector3IntGameEvent gameEvent;
        public Vector3IntUnityEvent response = new Vector3IntUnityEvent();

        #endregion

        #region Unity Methods

        private void OnEnable()
        {
            gameEvent.AddEventListener(this);
        }

        private void OnDisable()
        {
            gameEvent.RemoveEventListener(this);
        }

        #endregion

        #region Response Methods

        public void OnEventRaised(Vector3Int argument)
        {
            response.Invoke(argument);
        }

        #endregion
    }
}

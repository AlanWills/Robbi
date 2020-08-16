using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Robbi.Events
{
    public class GameEventListener<TGameEvent, TUnityEvent> : MonoBehaviour
    {
        #region Properties and Fields

        public TGameEvent gameEvent;
        public TUnityEvent response;

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

        public void OnEventRaised()
        {
            response.Invoke();
        }

        #endregion
    }
}
